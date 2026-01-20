using Contracts.IRepository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repository.BaseManager;

namespace Repositories.Repository
{
    public class EquipmentRepository : RepositoryBase<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(RepositoryContext context)
            : base(context) { }

        public async Task<IEnumerable<Equipment>> GetEquipmentAsync()
        {
            return await FindAll(trackChanges: false).Include(e => e.Category).ToListAsync();
        }

        public async Task<Equipment?> GetEquipmentByIdAsync(int id,bool trackchange)
        {
            return await FindByCondition(e => e.EquipmentId == id, trackchange)
                .FirstOrDefaultAsync();
        }

        public void CreateEquipment(Equipment equipment)
        {
            Create(equipment);
        }

        public void UpdateEquipment(Equipment equipment)
        {
            Update(equipment);
        }

        public async void DeleteEquipment(Equipment equipment)
        {
            Delete(equipment);
        }
    }
}
