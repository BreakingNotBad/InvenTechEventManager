using Contracts.IRepository.BaseManager;
using Entities.Models;
using Shared.RequestFeatures.Parameters;

namespace Contracts.IRepository
{
    public interface IStaffRepository : IRepositoryBase<Staff>
    {
        Task<IEnumerable<Staff>> GetStaffMembersAsync(StaffParameter staffParameter, bool trackChanges);
        Task<Staff?> GetStaffByIdAsync(int id,bool trackchange);
        Task<IEnumerable<Staff>> GetStaffByEventIdAsync(int eventId);
        void CreateStaff(Staff staff);
        void UpdateStaff(Staff staff);
        void DeleteStaff(Staff staff);
    }
}
