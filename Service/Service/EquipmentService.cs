using Contracts.IRepository.BaseManager;
using Entities.Models;
using Service.Contracts.DTOs.Equipment;
using Service.Contracts.IService;

namespace Service.Service
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IRepositoryManager _repo;

        public EquipmentService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Equipment>> GetEquipmentAsync(
            string? equipmentName,
            string? category,
            bool? IsDeleted,
            DateTime CreatedAt,
            DateTime UpdatedAt
        )
        {
            var equipmentList = await _repo.Equipment.GetEquipmentAsync();

            //  search
            if (!string.IsNullOrWhiteSpace(equipmentName))
            {
                equipmentList = equipmentList.Where(e =>
                    e.EquipmentName.ToLower().Contains(equipmentName.ToLower())
                );
            }

            //  filter category
            if (!string.IsNullOrWhiteSpace(category))
            {
                equipmentList = equipmentList.Where(e =>
                e.Category.CategoryName.ToLower().Contains(category.ToLower())
                );
            }

            return equipmentList;
        }

        public async Task<Equipment?> GetEquipmentByIdAsync(int id)
        {
            return await _repo.Equipment.GetEquipmentByIdAsync(id,false);
        }

        public async Task<Equipment> CreateEquipmentAsync(CreateEquipmentDto dto)
        {

            var equipment = new Equipment
            {
                EquipmentName = dto.EquipmentName,
                CategoryId = dto.CategoryId,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            _repo.Equipment.CreateEquipment(equipment);
            await _repo.SaveAsync();

            return equipment;
        }


        public async Task<Equipment> UpdateEquipmentAsync(int id, UpdateEquipmentDto dto)
        {
            var existingEquipment = await _repo.Equipment.GetEquipmentByIdAsync(id,true);
            if (existingEquipment == null)
            {
                throw new KeyNotFoundException($"Equipment with id {id} not found.");
            }
            // Update fields
            existingEquipment.EquipmentName = dto.EquipmentName;
            existingEquipment.CategoryId = dto.CategoryId;
            existingEquipment.IsDeleted = dto.IsDeleted;
            existingEquipment.UpdatedAt = DateTime.UtcNow;

            _repo.Equipment.UpdateEquipment(existingEquipment);
            await _repo.SaveAsync();
            return existingEquipment;
        }

        public async Task DeleteEquipment(int id)
        {
            var existingEquipment = await _repo.Equipment.GetEquipmentByIdAsync(id,true);
            if (existingEquipment == null)
            {
                throw new KeyNotFoundException($"Equipment with id {id} not found.");
            }
            _repo.Equipment.DeleteEquipment(existingEquipment);
            await _repo.SaveAsync();
        }
    }
}
