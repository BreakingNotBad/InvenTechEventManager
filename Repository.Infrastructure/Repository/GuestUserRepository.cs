using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Repositories;

namespace Repository.Infrastructure.Repository
{
    public class GuestUserRepository : RepositoryBase<GuestUser>, IGuestUserRepository
    {
        public GuestUserRepository(RepositoryContext context) : base(context)
        {
        }

        public async Task<IEnumerable<GuestUser>> GetGuestUsersAsync()
        {
            return await FindAll(trackChanges: false).ToListAsync();
        }

        public async Task<GuestUser?> GetGuestUserByIdAsync(int id)
        {
            return await FindByCondition(e => e.GuestUserId == id, trackChanges: false)
                .FirstOrDefaultAsync();
        }
    }
}
