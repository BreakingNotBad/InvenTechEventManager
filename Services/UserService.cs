using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Service.Contract;

namespace Service
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _repo;

        public UserService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _repo.User.GetUsersAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _repo.User.GetUserByIdAsync(id);
        }
    }
}
