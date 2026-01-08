using Contract.Interfaces.IRepository.BaseManager;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Service.Contract;

namespace Service
{
    public class EquipmentService : IEquipmentService
    {
        private readonly IRepositoryManager _repo;

        public EquipmentService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Equipment>> GetEquipmentAsync(
            string? query,
            string? category)
        {
            var equipment = await _repo.Equipment.GetEquipmentAsync();

            //  search (q) - case-insensitive
            if (!string.IsNullOrWhiteSpace(query))
            {
                equipment = equipment.Where(e =>
                    e.EquipmentName.Contains(query, StringComparison.OrdinalIgnoreCase));
            }

            //  filter category 
            if (!string.IsNullOrWhiteSpace(category))
            {
                equipment = equipment.Where(e =>
                    e.Category != null &&
                    e.Category.CategoryName.Contains(category, StringComparison.OrdinalIgnoreCase));
            }

            return equipment;
        }

        public async Task<Equipment?> GetEquipmentByIdAsync(int id)
        {
            return await _repo.Equipment.GetEquipmentByIdAsync(id);
        }

        public async Task CreateEquipmentAsync(Equipment equipment)
        {
            _repo.Equipment.CreateEquipment(equipment);
            await _repo.SaveAsync();
        }

        public async Task UpdateEquipmentAsync(int id, Equipment equipment)
        {
            var existingEquipment = await _repo.Equipment.GetEquipmentByIdAsync(id);
            if (existingEquipment == null)
            {
                throw new KeyNotFoundException($"Equipment with id {id} not found.");
            }

            existingEquipment.EquipmentName = equipment.EquipmentName;
            existingEquipment.CategoryId = equipment.CategoryId;
            existingEquipment.UpdatedAt = DateTime.UtcNow;

            _repo.Equipment.UpdateEquipment(existingEquipment);
            await _repo.SaveAsync();
        }

        public async Task DeleteEquipment(int id)
        {
            var existingEquipment = await _repo.Equipment.GetEquipmentByIdAsync(id);
            if (existingEquipment == null)
            {
                throw new KeyNotFoundException($"Equipment with id {id} not found.");
            }
            _repo.Equipment.DeleteEquipment(existingEquipment);
            await _repo.SaveAsync();
        }
    }
}
