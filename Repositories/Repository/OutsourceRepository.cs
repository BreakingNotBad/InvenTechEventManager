using Contracts.IRepository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repository.BaseManager;

namespace Repositories.Repository
{
    public class OutsourceRepository : RepositoryBase<Outsource>, IOutsourceRepository
    {
        public OutsourceRepository(RepositoryContext context)
            : base(context) { }

        public async Task<IEnumerable<Outsource>> GetOutsourceAsyn()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public async Task<Outsource?> GetOutsourceByIdAsync(int id, bool trackchange)
        {
            return await FindByCondition(e => e.OutsourceId == id, trackchange)
                .FirstOrDefaultAsync();
        }

        public void CreateOutsource(Outsource outsource)
        {
            Create(outsource);
        }

        public void UpdateOutsource(Outsource outsource)
        {
            Update(outsource);
        }

        public async void DeleteOutsource(Outsource outsource)
        {
            Delete(outsource);
        }
    }
}
