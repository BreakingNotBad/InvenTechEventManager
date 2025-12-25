using Entity.Domain.Model;

namespace Contract.Interfaces.IRepository
{
    public interface IOutsourceRepository : IRepositoryBase<Outsource>
    {
        Task<IEnumerable<Outsource>> GetGuestUsersAsync();
        Task<Outsource?> GetGuestUserByIdAsync(int id);
    }
}
