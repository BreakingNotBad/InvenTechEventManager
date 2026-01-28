using Contracts.IRepository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repository.BaseManager;
using Shared.RequestFeatures.Parameters;

namespace Repositories.Repository
{
    public class OutsourceRepository : RepositoryBase<Outsource>, IOutsourceRepository
    {
        public OutsourceRepository(RepositoryContext context)
            : base(context) { }

        public async Task<IEnumerable<Outsource>> GetOutsourceAsyn(OutsourceParameter outsourceParameter ,bool trackChanges)
        {
            // 1. เริ่มต้น Query
            var items = FindAll(trackChanges);

            // 2. Filter: Search by FullName
            if (!string.IsNullOrWhiteSpace(outsourceParameter.FullName))
            {
                items = items.Where(o => o.FullName.ToLower().Contains(outsourceParameter.FullName.ToLower()));
            }

            // 3. Execute Query พร้อมเรียงลำดับ (ควรเรียงเสมอเพื่อให้ข้อมูลไม่กระโดด)
            return await items
                .ToListAsync();
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
