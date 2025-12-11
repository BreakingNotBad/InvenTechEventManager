using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Service.Contract;

namespace Service
{
    public class EquipmentSetContentService : IEquipmentSetContentService
    {
        private readonly IRepositoryManager _repo;

        public EquipmentSetContentService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<EquipmentSetContent>> GetEquipmentSetContentsAsync()
        {
            return await _repo.EquipmentSetContent.GetEquipmentSetContentsAsync();
        }

        public async Task<IEnumerable<EquipmentSetContent>> GetContentsByEquipmentSetIdAsync(int equipmentSetId)
        {
            return await _repo.EquipmentSetContent.FindByCondition(
                c => c.EquipmentSetId == equipmentSetId,
                trackChanges: false
            ).ToListAsync();
        }
    }
}
