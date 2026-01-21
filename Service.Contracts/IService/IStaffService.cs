using Entities.Models;
using Service.Contracts.DTOs.Staff;

namespace Service.Contracts.IService
{
    public interface IStaffService
    {
        Task<IEnumerable<Staff>> GetStaffMembersAsync(
            string? fullName,
            string? status,
            string? role,
            bool? available,
            DateTime? date,
            string? period
        );
        Task<Staff?> GetStaffByIdAsync(int id);
        Task<Staff> CreateStaffAsync(
            CreateStaffDto staffDto,
            Stream? avatarStream,
            string? avatarFileName
        );
        Task UpdateStaffAsync(
            int id,
            UpdateStaffDto dto,
            Stream? avatarStream,
            string? avatarFileName
        );
        Task DeleteStaffAsync(int id);
        Task SoftDeleteStaffAsync(int id, bool isDeleted);
    }
}
