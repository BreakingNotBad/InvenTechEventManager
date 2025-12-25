using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository
{
    public interface IStaffRepository : IRepositoryBase<Staff>
    {
        Task<IEnumerable<Staff>> GetStaffMembersAsync();
        Task<Staff?> GetStaffByIdAsync(int id);
        Task<IEnumerable<Staff>> GetStaffByEventIdAsync(int eventId);
        Task<IEnumerable<Staff>> GetStaffActiveAsync(
            string? search,
            DateOnly? date,
            string? time_period,
            Boolean? filter_available
        );
        void CreateStaff(Staff staff);
        void UpdateStaff(Staff staff);
        void DeleteStaff(Staff staff);
    }
}
