using Contracts.IRepository.BaseManager;
using Entities.Models;
using Service.Contracts.DTOs.Package;
using Service.Contracts.IService;

namespace Service.Service
{
    public class PackageService : IPackageService
    {
        private readonly IRepositoryManager _repo;

        public PackageService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Package>> GetPackagesAsync()
        {
            return await _repo.Package.GetPackagesAsync();
        }

        public async Task<Package?> GetPackageByIdAsync(int id)
        {
            return await _repo.Package.GetPackageByIdAsync(id,false);
        }

        public async Task<Package> CreatePackageAsync(CreatePackageDto dto)
        {
            var package = new Package
            {
                PackageName = dto.PackageName,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow
            };

            // สร้าง EquipmentSet
            foreach (var item in dto.EquipmentSets)
            {
                package.EquipmentSets.Add(new EquipmentSet
                {
                    EquipmentId = item.EquipmentId,
                    Quantity = item.Quantity
                });
            }
            _repo.Package.CreatePackage(package);
            await _repo.SaveAsync();
            return package;
        }

        public async Task<Package> UpdatePackageAsync(int id, UpdatePackageDto dto)
        {
            var existingPackage = await _repo.Package.GetPackageByIdAsync(id,true); // ดึงข้อมูลพร้อม EquipmentSets
            if (existingPackage == null) // ตรวจสอบว่าพบ Package หรือไม่
            {
                throw new KeyNotFoundException($"Package with id {id} not found.");
            }
            // update fields
            existingPackage.PackageName = dto.PackageName;
            existingPackage.UpdatedAt = DateTime.UtcNow;

            // Clear existing EquipmentSets
            existingPackage.EquipmentSets.Clear();

            // เพิ่มของใหม่
            foreach (var equipment in dto.EquipmentSets) // ตรวจสอบ EquipmentId ว่ามีอยู่จริงหรือไม่
            {
                var equipmentExists = await _repo.Equipment.GetEquipmentByIdAsync(equipment.EquipmentId,false); //
                if (equipmentExists == null) // ถ้าไม่พบ Equipment
                    throw new Exception($"Equipment {equipment.EquipmentId} not found");

                existingPackage.EquipmentSets.Add(new EquipmentSet // เพิ่ม EquipmentSet ใหม่
                {
                    EquipmentId = equipment.EquipmentId,
                    Quantity = equipment.Quantity
                });
            }

            _repo.Package.UpdatePackage(existingPackage);
            await _repo.SaveAsync();
            return existingPackage;
        }

        public async Task DeletePackage(int id)
        {
            var existingPackage = await _repo.Package.GetPackageByIdAsync(id,true);
            if (existingPackage == null)
            {
                throw new KeyNotFoundException($"Package with id {id} not found.");
            }
            _repo.Package.DeletePackage(existingPackage);
            await _repo.SaveAsync();
        }
    }
}
