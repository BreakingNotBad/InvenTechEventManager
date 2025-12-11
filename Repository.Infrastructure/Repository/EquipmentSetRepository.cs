using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Repositories;

namespace Repository.Infrastructure.Repository
{
    public class EquipmentSetRepository : RepositoryBase<EquipmentSet>, IEquipmentSetRepository
    {
        public EquipmentSetRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<EquipmentSet>> GetEquipmentSetsAsync()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public async Task<EquipmentSet?> GetEquipmentSetByIdAsync(int id)
        {
            return await FindByCondition(e => e.EquipmentSetId == id, trackChanges: false)
                .FirstOrDefaultAsync();
        }
    }
}
