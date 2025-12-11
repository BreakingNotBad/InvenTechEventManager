using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Repositories;

namespace Repository.Infrastructure.Repository
{
    public class EquipmentRepository : RepositoryBase<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Equipment>> GetEquipmentAsync()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public async Task<Equipment?> GetEquipmentByIdAsync(int id)
        {
            return await FindByCondition(e => e.EquipmentId == id, trackChanges: false)
                .FirstOrDefaultAsync();
        }
    }
}
