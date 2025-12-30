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

        public async Task<IEnumerable<Equipment>> GetEquipmentAsync()
        {
            return await _repo.Equipment.GetEquipmentAsync();
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
