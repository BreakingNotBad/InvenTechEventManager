using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IStaffService
    {
        Task<IEnumerable<Staff>> GetStaffMembersAsync();
        Task<Staff?> GetStaffByIdAsync(int id);
        Task CreateStaffAsync(Staff staff);
        Task DeleteStaffAsync(int id);
    }
}
