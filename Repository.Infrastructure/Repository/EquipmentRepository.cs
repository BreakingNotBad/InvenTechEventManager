using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Repositories;

namespace Repository.Infrastructure.Repository
{
    public class EquipmentRepository : RepositoryBase<Equipments>, IEquipmentRepository
    {
        public EquipmentRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Equipments>> GetEquipmentAsync()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public async Task<Equipments?> GetEquipmentByIdAsync(int id)
        {
            return await FindByCondition(e => e.EquipmentId == id, trackChanges: false)
                .FirstOrDefaultAsync();
        }
        public void CreateEquipment(Equipments equipment)
        {
            Create(equipment);
        }
        public async void DeleteEquipment(Equipments equipment)
        {
            Delete(equipment);
            await Task.CompletedTask;
        }
    }
}
