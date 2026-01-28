using Service.Contracts.DTOs.Staff;
using Shared.RequestFeatures.Parameters;

namespace Service.Contracts.IService
{
    public interface IStaffService
    {
        Task<IEnumerable<StaffDto>> GetStaffMembersAsync(StaffParameter staffParameter
        );
        Task<StaffDto?> GetStaffByIdAsync(int id);
        Task<StaffDto> CreateStaffAsync(CreateStaffDto staffDto);
        Task UpdateStaffAsync(int id, UpdateStaffDto staffDto);
        Task DeleteStaffAsync(int id);
        Task SoftDeleteStaffAsync(int id, bool isDeleted);
    }
}
