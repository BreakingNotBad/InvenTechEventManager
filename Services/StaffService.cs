using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Service.Contract;

namespace Service
{
    public class StaffService : IStaffService
    {
        private readonly IRepositoryManager _repo;

        public StaffService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Staff>> GetStaffMembersAsync()
        {
            return await _repo.Staff.GetStaffMembersAsync();
        }

        public async Task<Staff?> GetStaffByIdAsync(int id)
        {
            return await _repo.Staff.GetStaffByIdAsync(id);
        }

    }
}
