using Contracts.IRepository.BaseManager;
using Entities.Models;
using Service.Contracts;
using Service.Contracts.DTOs.Package;
using Service.Contracts.IService;

namespace Service
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
            return await _repo.Package.GetPackageByIdAsync(id, false);
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
            var existingPackage = await _repo.Package.GetPackageByIdAsync(id, true); // ดึงข้อมูลพร้อม EquipmentSets
            if (existingPackage == null) // ตรวจสอบว่าพบ Package หรือไม่
            {
                throw new KeyNotFoundException($"Package with id {id} not found.");
            }
            // update fields
            existingPackage.PackageName = dto.PackageName;
            existingPackage.UpdatedAt = DateTime.UtcNow;

            var existingSets = existingPackage.EquipmentSets.ToList(); // รายชื่อของที่มีอยู่ตอนนี้ออกมาเก็บไว้ดู เพื่อจะได้เปรียบเทียบ เอาไว้ ลบ / แก้ / เพิ่ม

            foreach (var set in existingSets) // ลูปเช็คว่าอันไหนต้องลบ
            {
                if (!dto.EquipmentSets.Any(d => d.EquipmentId == set.EquipmentId)) // ดูของเก่าทีละชิ้นถ้าชิ้นไหน ไม่อยู่ในรายการใหม่ ให้ลบ
                    existingPackage.EquipmentSets.Remove(set); // ลบออกจากรายการ
            }

            foreach (var Set in dto.EquipmentSets) // ดูของใหม่ทีละชิ้น ลูปเช็คว่าอันไหนต้องแก้ไข หรือเพิ่ม
            {
                var existingSet = existingSets
                    .FirstOrDefault(x => x.EquipmentId == Set.EquipmentId); // หาในรายการเก่าว่ามีชิ้นนี้อยู่ไหม

                if (existingSet != null) // ถ้ามีอยู่แล้ว ให้แก้ไขจำนวน
                {
                    existingSet.Quantity = Set.Quantity;
                }
                else // ถ้าไม่มีอยู่ในรายการเก่า ให้เพิ่มใหม่
                {
                    var equipmentExists = await _repo.Equipment // เช็คก่อนว่า อุปกรณ์ที่เพิ่มมานั้น มีอยู่จริงไหม
                        .GetEquipmentByIdAsync(Set.EquipmentId, false); // ดึงข้อมูลอุปกรณ์

                    if (equipmentExists == null)
                        throw new Exception($"Equipment {Set.EquipmentId} not found");

                    existingPackage.EquipmentSets.Add(new EquipmentSet // เพิ่มใหม่
                    {
                        EquipmentId = Set.EquipmentId,
                        Quantity = Set.Quantity
                    });
                }
            }

            _repo.Package.UpdatePackage(existingPackage);
            await _repo.SaveAsync();
            return existingPackage;
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
