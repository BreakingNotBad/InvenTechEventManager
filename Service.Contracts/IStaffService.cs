using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IStaffService
    {
        Task<IEnumerable<Staff>> GetStaffMembersAsync();
        Task<Staff?> GetStaffByIdAsync(int id);
        Task<IEnumerable<Staff>> GetStaffActiveAsync(
            string? search,
            DateOnly? date,
            string? time_period,
            Boolean? filter_available
        );
        Task CreateStaffAsync(Staff staff);
        Task UpdateStaffAsync(int id, Staff staff);
        Task DeleteStaffAsync(int id);
    }
}
