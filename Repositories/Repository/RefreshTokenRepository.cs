using Contracts.IRepository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repository.BaseManager;

namespace Repositories.Repository
{
    public class RefreshTokenRepository 
        : RepositoryBase<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(RepositoryContext context)
            : base(context)
        {
        }

        // CREATE
        public async Task CreateAsync(RefreshToken token)
        {
             Create(token);
        }

        // GET BY TOKEN
        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await FindByCondition(
                x => x.Token == token &&
                     !x.IsRevoked &&
                     x.ExpiresAt > DateTime.UtcNow,
                false)
                .Include(x => x.Staff)
                    .ThenInclude(s => s.StaffRoles)
                        .ThenInclude(sr => sr.Role)
                .Include(x => x.Staff)
                    .ThenInclude(s => s.StaffPermissions)
                        .ThenInclude(sp => sp.Permission)
                .SingleOrDefaultAsync();
        }

        // UPDATE (REVOKE)
        public async Task UpdateAsync(RefreshToken token)
        {
            Update(token);
        }
    }
}
