using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository
{
    public interface IGuestUserRepository : IRepositoryBase<GuestUser>
    {
        Task<IEnumerable<GuestUser>> GetGuestUsersAsync();
        Task<GuestUser?> GetGuestUserByIdAsync(int id);
    }
}
