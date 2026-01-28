using AutoMapper;
using Contracts.IRepository.BaseManager;
using Entities.Exceptions;
using Entities.Models;
using FluentValidation;
using Service.Contracts;
using Service.Contracts.DTOs.Package;
using Service.Contracts.IService;
using Shared.RequestFeatures.Parameters;

namespace Service
{
    public class PackageService : IPackageService
    {
        private readonly IRepositoryManager _repo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreatePackageDto> _createValidator;
        private readonly IValidator<UpdatePackageDto> _updateValidator;

        public PackageService(
            IRepositoryManager repo,
            IMapper mapper,
            IValidator<CreatePackageDto> createValidator,
            IValidator<UpdatePackageDto> updateValidator)
        {
            _repo = repo;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<IEnumerable<PackageDto>> GetPackagesAsync(PackageParameter packageParameter)
        {
            var packages = await _repo.Package.GetPackagesAsync(packageParameter,false);
            return _mapper.Map<IEnumerable<PackageDto>>(packages);
        }

        public async Task<PackageDto?> GetPackageByIdAsync(int id)
        {
            var existingPackage = await _repo.Package.GetPackageByIdAsync(id, false);
            if (existingPackage == null)
            {
                throw new NotFoundException(nameof(Package), id);
            }
            var packageReponse = _mapper.Map<PackageDto>(existingPackage);
            return packageReponse;
        }

        public async Task<PackageDto> CreatePackageAsync(CreatePackageDto packageDto)
        {
            await _createValidator.ValidateAndThrowAsync(packageDto);
            var NewPackage = _mapper.Map<Package>(packageDto); 

            // สร้าง EquipmentSet
            foreach (var item in packageDto.EquipmentSets)
            {
                NewPackage.EquipmentSets.Add(new EquipmentSet
                {
                    EquipmentId = item.EquipmentId,
                    Quantity = item.Quantity
                });
            }
            _repo.Package.CreatePackage(NewPackage);
            await _repo.SaveAsync();

            var package = await GetPackageByIdAsync(NewPackage.PackageId);

            return package!;
        }

        public async Task<PackageDto> UpdatePackageAsync(int id, UpdatePackageDto packageDto)
        {
            await _updateValidator.ValidateAndThrowAsync(packageDto);
            var existingPackage = await _repo.Package.GetPackageByIdAsync(id, true); // ดึงข้อมูลพร้อม EquipmentSets
            if (existingPackage == null) // ตรวจสอบว่าพบ Package หรือไม่
            {
                throw new NotFoundException(nameof(Package), id);
            }
            // update fields
            _mapper.Map(packageDto, existingPackage);
            existingPackage.UpdatedAt = DateTime.UtcNow;

            var existingSets = existingPackage.EquipmentSets.ToList(); // รายชื่อของที่มีอยู่ตอนนี้ออกมาเก็บไว้ดู เพื่อจะได้เปรียบเทียบ เอาไว้ ลบ / แก้ / เพิ่ม

            foreach (var set in existingSets) // ลูปเช็คว่าอันไหนต้องลบ 
            {
                if (!packageDto.EquipmentSets.Any(d => d.EquipmentId == set.EquipmentId)) // ดูของเก่าทีละชิ้นถ้าชิ้นไหน ไม่อยู่ในรายการใหม่ ให้ลบ
                    existingPackage.EquipmentSets.Remove(set); // ลบออกจากรายการ
            }

            foreach (var set in packageDto.EquipmentSets) // ดูของใหม่ทีละชิ้น ลูปเช็คว่าอันไหนต้องแก้ไข หรือเพิ่ม
            {
                var existingSet = existingSets
                    .FirstOrDefault(x => x.EquipmentId == set.EquipmentId); // หาในรายการเก่าว่ามีชิ้นนี้อยู่ไหม

                if (existingSet != null) // ถ้ามีอยู่แล้ว ให้แก้ไขจำนวน
                {
                    existingSet.Quantity = set.Quantity;
                }
                else // ถ้าไม่มีอยู่ในรายการเก่า ให้เพิ่มใหม่
                {
                    var equipmentExists = await _repo.Equipment // เช็คก่อนว่า อุปกรณ์ที่เพิ่มมานั้น มีอยู่จริงไหม
                        .GetEquipmentByIdAsync(set.EquipmentId, false); // ดึงข้อมูลอุปกรณ์

                    if (equipmentExists == null)
                        throw new Exception($"Equipment {set.EquipmentId} not found");

                    existingPackage.EquipmentSets.Add(new EquipmentSet // เพิ่มใหม่
                    {
                        EquipmentId = set.EquipmentId,
                        Quantity = set.Quantity
                    });
                }
            }

            _repo.Package.UpdatePackage(existingPackage);
            await _repo.SaveAsync();
            var packageReponse = _mapper.Map<PackageDto>(existingPackage);
            return packageReponse;
        }

        public async Task DeletePackage(int id)
        {
            var existingPackage = await _repo.Package.GetPackageByIdAsync(id, true);
            if (existingPackage == null)
            {
                throw new KeyNotFoundException($"Package with id {id} not found.");
            }
            _repo.Package.DeletePackage(existingPackage);
            await _repo.SaveAsync();
        }
    }
}
