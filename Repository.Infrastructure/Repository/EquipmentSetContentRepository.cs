using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Repositories;

namespace Repository.Infrastructure.Repository
{
    public class EquipmentSetContentRepository : RepositoryBase<EquipmentSetContent>, IEquipmentSetContentRepository
    {
        public EquipmentSetContentRepository(RepositoryContext context) : base(context)
        {
        }
        public async Task<IEnumerable<EquipmentSetContent>> GetEquipmentSetContentsAsync()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }
        public async Task<IEnumerable<EquipmentSetContent>> GetContentsByEquipmentSetIdAsync(int equipmentSetId)
        {
            return await FindByCondition(c => c.EquipmentSetId == equipmentSetId, trackChanges: false)
                .ToListAsync();
        }

    }
}
