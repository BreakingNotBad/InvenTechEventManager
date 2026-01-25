using Service.Contracts.DTOs.Staff;

namespace Service.Contracts.IService
{
    public interface IStaffService
    {
        Task<IEnumerable<StaffDto>> GetStaffMembersAsync(
            string? fullName,
            string? status,
            string? role,
            bool? available,
            DateTime? date,
            string? period
        );
        Task<StaffDto?> GetStaffByIdAsync(int id);
        Task<StaffDto> CreateStaffAsync(CreateStaffDto staffDto);
        Task UpdateStaffAsync(int id, UpdateStaffDto staffDto);
        Task DeleteStaffAsync(int id);
        Task SoftDeleteStaffAsync(int id, bool isDeleted);
    }
}
