using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Infrastructure.Repository.BaseManager;

namespace Repository.Infrastructure.Repository
{
    public class OutsourceRepository : RepositoryBase<Outsource>, IOutsourceRepository
    {
        public OutsourceRepository(RepositoryContext context)
            : base(context) { }

        public async Task<IEnumerable<Outsource>> GetOutsourceAsyn()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public async Task<Outsource?> GetOutsourceByIdAsync(int id)
        {
            return await FindByCondition(e => e.OutsourceId == id, trackChanges: false)
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
