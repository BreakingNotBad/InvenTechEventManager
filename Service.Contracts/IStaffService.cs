using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IStaffService
    {
        Task<IEnumerable<Staff>> GetStaffMembersAsync();
        Task<Staff?> GetStaffByIdAsync(int id);
        Task<IEnumerable<Staff>> GetStaffActiveAsync();
        Task CreateStaffAsync(Staff staff);
        Task UpdateStaffAsync(int id, Staff staff);
        Task DeleteStaffAsync(int id);
    }
}
