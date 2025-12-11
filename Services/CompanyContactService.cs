using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Service.Contract;
using Microsoft.EntityFrameworkCore;


namespace Service
{
    
    public class CompanyContactService : ICompanyContactService
    {
        private readonly IRepositoryManager _repo;
        public CompanyContactService(IRepositoryManager repo)
        {
            _repo = repo;
        }
        public async Task<IEnumerable<CompanyContact>> GetCompanyContactsAsync()
        {
            return await _repo.CompanyContact.GetCompanyContactsAsync();
        }
        public async Task<CompanyContact?> GetCompanyContactByIdAsync(int id)
        {
            var query = _repo.CompanyContact.FindByCondition(
                e => e.CompanyContactId == id,
                trackChanges: false
            );
            return await query.FirstOrDefaultAsync();
        }
    }
}
