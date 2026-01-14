using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IStaffService
    {
        Task<IEnumerable<Staff>> GetStaffMembersAsync(
            string? fullName, 
            string? status, 
            string? role,
            bool? available,
            DateTime? date,
            string? period);
        Task<Staff?> GetStaffByIdAsync(int id);
        Task CreateStaffAsync(Staff staff, List<int> roleIds);
        Task UpdateStaffAsync(int id, Staff staff);
        Task DeleteStaffAsync(int id);
        Task SoftDeleteStaffAsync(int id, bool isDeleted);
    }
}
