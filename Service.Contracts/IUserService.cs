using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
    }
}
