using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Service.Contract;

namespace Service
{
    public class OutsourceService : IOutsourceService
    {
        private readonly IRepositoryManager _repo;

        public OutsourceService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Outsource>> GetGuestUsersAsync()
        {
            return await _repo.Outsource.GetGuestUsersAsync();
        }

        public async Task<Outsource?> GetGuestUserByIdAsync(int id)
        {
            return await _repo.Outsource.GetGuestUserByIdAsync(id);
        }
    }
}
