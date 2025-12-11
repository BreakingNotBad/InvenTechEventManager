using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IGuestUserService
    {
        Task<IEnumerable<GuestUser>> GetGuestUsersAsync();
        Task<GuestUser?> GetGuestUserByIdAsync(int id);
    }
}
