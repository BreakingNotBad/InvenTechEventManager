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

        public async Task<IEnumerable<Equipment>> GetEquipmentAsync()
        {
            return await _repo.Equipment.GetEquipmentAsync();
        }

        public async Task<Equipment?> GetEquipmentByIdAsync(int id)
        {
            var query = _repo.Equipment.FindByCondition(
                e => e.EquipmentId == id,
                trackChanges: false
            );

            return await query.FirstOrDefaultAsync();
        }

    }
}
