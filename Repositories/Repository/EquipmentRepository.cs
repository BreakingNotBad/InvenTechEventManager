using Contracts.IRepository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repository.BaseManager;
using Shared.RequestFeatures.Parameters;

namespace Repositories.Repository
{
    public class EquipmentRepository : RepositoryBase<Equipment>, IEquipmentRepository
    {
        public EquipmentRepository(RepositoryContext context)
            : base(context) { }

        public async Task<IEnumerable<Equipment>> GetEquipmentAsync(EquipmentParameter equipmentParameter, bool trackChanges)
        {
            // 1. เตรียม Query (ยังไม่ยิง Database)
            var items = FindAll(trackChanges);

            // 2. Filter: EquipmentName
            if (!string.IsNullOrWhiteSpace(equipmentParameter.EquipmentName))
            {
                items = items.Where(e => e.EquipmentName.ToLower().Contains(equipmentParameter.EquipmentName.ToLower()));
            }

            // 3. Filter: Category (Relation)
            // EF Core ฉลาดพอที่จะ join ตารางให้เองเมื่อเราอ้างถึง e.Category.CategoryName ใน Where
            if (!string.IsNullOrWhiteSpace(equipmentParameter.Category))
            {
                items = items.Where(e => e.Category.CategoryName.ToLower().Contains(equipmentParameter.Category.ToLower()));
            }

            // 4. Filter: IsDeleted
            if (equipmentParameter.IsDeleted.HasValue)
            {
                items = items.Where(e => e.IsDeleted == equipmentParameter.IsDeleted.Value);
            }

            // 5. Filter: Dates
            if (equipmentParameter.CreatedAt != default(DateTime))
            {
                items = items.Where(e => e.CreatedAt.Date == equipmentParameter.CreatedAt.Date);
            }

            if (equipmentParameter.UpdatedAt != default(DateTime))
            {
                items = items.Where(e => e.UpdatedAt.HasValue && e.UpdatedAt.Value.Date == equipmentParameter.UpdatedAt.Date);
            }

            // 6. Execute Query
            // ใส่ Include เพื่อดึงข้อมูล Category ออกมาด้วย
            return await items
                .Include(e => e.Category)
                .ToListAsync();
        }

        public async Task<Equipment?> GetEquipmentByIdAsync(int id,bool trackchange)
        {
            return await FindByCondition(e => e.EquipmentId == id, trackchange)
                .Include(e => e.Category)
                .FirstOrDefaultAsync();
        }
        public async Task<bool> ExistsAsync(int equipmentId)
        {
            return await FindByCondition(
                e => e.EquipmentId == equipmentId,
                trackChanges: false
            ).AnyAsync();
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
