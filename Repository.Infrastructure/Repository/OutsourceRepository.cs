using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Repositories;

namespace Repository.Infrastructure.Repository
{
    public class OutsourceRepository : RepositoryBase<Outsources>, IOutsourceRepository
    {
        public OutsourceRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Outsources>> GetOutsourceAsyn()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public async Task<Outsources?> GetOutsourceByIdAsync(int id)
        {
            return await FindByCondition(e => e.OutsourceId == id, trackChanges: false)
                .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Outsources>> GetOutsourceActiveAsync(
            string? search,
            DateOnly? date,
            string? time_period,
            Boolean? filter_available
        )
        {
            // ???????? Query Outsource ???????
            var query = FindAll(trackChanges: false);
            // ???? Search
            if (!string.IsNullOrWhiteSpace(search))
            {
                var lowerName = search.Trim().ToLower();
                query = query.Where(s => s.Fullname.ToLower().Contains(lowerName));
            }
            // ???? Query ?????????????
            return await query.OrderBy(s => s.Fullname).ToListAsync();
        }
        public void Createoutsource(Outsources outsource)
        {
            Create(outsource);
        }
        public void Updateoutsource(Outsources outsource)
        {
            Update(outsource);
        }
        public async void Deleteoutsource(Outsources outsource)
        {
            Delete(outsource);
            await Task.CompletedTask;
        }
    }
}
