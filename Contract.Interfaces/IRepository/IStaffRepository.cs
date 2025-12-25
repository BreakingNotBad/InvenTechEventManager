using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository
{
    public interface IStaffRepository : IRepositoryBase<Staff>
    {
        Task<IEnumerable<Staff>> GetStaffMembersAsync();
        Task<Staff?> GetStaffByIdAsync(int id);
        Task<IEnumerable<Staff>> GetStaffByEventIdAsync(int eventId);
        void CreateStaff(Staff staff);
        void DeleteStaff(Staff staff);
    }
}
