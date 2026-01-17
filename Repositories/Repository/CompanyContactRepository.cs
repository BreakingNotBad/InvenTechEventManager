using Contracts.IRepository;
using Entities.Models;
using Repositories.Data;
using Repositories.Repository.BaseManager;

namespace Repositories.Repository
{
    public class CompanyContactRepository
        : RepositoryBase<CompanyContact>,
            ICompanyContactRepository
    {
        public CompanyContactRepository(RepositoryContext context)
            : base(context) { }

        public void DeleteCompanyContact(CompanyContact companyContact)
        {
            Delete(companyContact);
        }
    }
}
