using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Repositories;

namespace Repository.Infrastructure.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await FindByCondition(e => e.UserId == id, trackChanges: false)
                .FirstOrDefaultAsync();
        }
    }
}
