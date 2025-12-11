using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
    }
}
