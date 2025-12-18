using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Service.Contract;

namespace Service
{
    public class EquipmentSetService : IEquipmentSetService
    {
        private readonly IRepositoryManager _repo;

        public EquipmentSetService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<EquipmentSet>> GetEquipmentSetsAsync()
        {
            return await _repo.EquipmentSet.GetEquipmentSetsAsync();
        }

        public async Task<EquipmentSet?> GetEquipmentSetByIdAsync(int id)
        {
            return await _repo.EquipmentSet.GetEquipmentSetByIdAsync(id);
        }
    }
}
