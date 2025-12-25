using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Repositories;

namespace Repository.Infrastructure.Repository
{
    public class OutsourceRepository : RepositoryBase<Outsource>, IOutsourceRepository
    {
        public OutsourceRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Outsource>> GetOutsourceAsyn()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public async Task<Outsource?> GetOutsourceByIdAsync(int id)
        {
            return await FindByCondition(e => e.OutsourceId == id, trackChanges: false)
                .FirstOrDefaultAsync();
        }
        public void Createoutsource(Outsource outsource)
        {
            Create(outsource);
        }
        public void Updateoutsource(Outsource outsource)
        {
            Update(outsource);
        }
        public async void Deleteoutsource(Outsource outsource)
        {
            Delete(outsource);
            await Task.CompletedTask;
        }
    }
}
