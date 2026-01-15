using Contract.Interfaces.DTOs;
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
        Task<Staff> CreateStaffAsync(CreateStaffDto staffDto, Stream? avatarStream, string? avatarFileName);
        Task UpdateStaffAsync(int id, UpdateStaffRequest request);
        Task DeleteStaffAsync(int id);
        Task SoftDeleteStaffAsync(int id, bool isDeleted);
    }
}
