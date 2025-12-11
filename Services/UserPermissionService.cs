using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Service.Contract;

namespace Service
{
    public class UserPermissionService : IUserPermissionService
    {
        private readonly IRepositoryManager _repo;

        public UserPermissionService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<UserPermission>> GetUserPermissionsAsync()
        {
            return await _repo.UserPermission.GetUserPermissionsAsync();
        }

        public async Task<IEnumerable<UserPermission>> GetByUserIdAsync(int userId)
        {
            return await _repo.UserPermission.FindByCondition(
                up => up.UserId == userId,
                trackChanges: false
            ).ToListAsync();
        }
    }
}
