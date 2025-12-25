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

        public async Task<IEnumerable<Staff>> GetStaffActiveAsync(
            string? search,
            DateOnly? date,
            string? time_period,
            Boolean? filter_available
        )
        {
            return await _repo.Staff.GetStaffActiveAsync(
                search,
                date,
                time_period,
                filter_available
            );
        }

        public async Task CreateStaffAsync(Staff staff)
        {
            _repo.Staff.CreateStaff(staff);
            await _repo.SaveAsync();
        }

        public async Task UpdateStaffAsync(int id, Staff staff)
        {
            var exinstingStaff = await _repo.Staff.GetStaffByIdAsync(id);

            if (exinstingStaff == null)
            {
                throw new KeyNotFoundException($"Staff with id: {id} does not exist.");
            }

            _repo.Staff.UpdateStaff(exinstingStaff);
            await _repo.SaveAsync();
        }

        public async Task DeleteStaffAsync(int id)
        {
            var exinstingStaff = await _repo.Staff.GetStaffByIdAsync(id);

            if (exinstingStaff == null)
            {
                throw new KeyNotFoundException($"Staff with id: {id} does not exist.");
            }

            _repo.Staff.DeleteStaff(exinstingStaff);
            await _repo.SaveAsync();
        }
    }
}
