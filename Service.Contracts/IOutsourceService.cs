using Entity.Domain.Model;

namespace Service.Contract
{
    public interface IOutsourceService
    {
        Task<IEnumerable<Outsource>> GetGuestUsersAsync();
        Task<Outsource?> GetGuestUserByIdAsync(int id);
    }
}
