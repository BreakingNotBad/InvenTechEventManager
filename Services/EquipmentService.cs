using Contract.Interfaces.IRepository;
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

        public async Task<IEnumerable<Equipments>> GetEquipmentAsync()
        {
            return await _repo.Equipment.GetEquipmentAsync();
        }

        public async Task<Equipments?> GetEquipmentByIdAsync(int id)
        {
            return await _repo.Equipment.GetEquipmentByIdAsync(id);
        }
        public async Task CreateEquipmentAsync(Equipments equipment)
        {
            _repo.Equipment.CreateEquipment(equipment);
            await _repo.SaveAsync();
        }
        public async Task DeleteEquipment(int id)
        {
            var existingEquipment = await _repo.Equipment.GetEquipmentByIdAsync(id);
            if (existingEquipment == null)
            {
                throw new ArgumentException($"Equipment with id {id} not found.");
            }
            _repo.Equipment.DeleteEquipment(existingEquipment);
            await _repo.SaveAsync();
        }
    }
}
