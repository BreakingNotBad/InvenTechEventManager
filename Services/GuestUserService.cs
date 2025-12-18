using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Service.Contract;

namespace Service
{
    public class GuestUserService : IGuestUserService
    {
        private readonly IRepositoryManager _repo;

        public GuestUserService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<GuestUser>> GetGuestUsersAsync()
        {
            return await _repo.GuestUser.GetGuestUsersAsync();
        }

        public async Task<GuestUser?> GetGuestUserByIdAsync(int id)
        {
            return await _repo.GuestUser.GetGuestUserByIdAsync(id);
        }
    }
}
