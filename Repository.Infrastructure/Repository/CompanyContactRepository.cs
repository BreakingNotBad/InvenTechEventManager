using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure.Data;
using Repository.Infrastructure.Repository.BaseManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Repository.Infrastructure.Repository
{
    public class CompanyContactRepository : RepositoryBase<CompanyContact>, ICompanyContactRepository
    {
        public CompanyContactRepository(RepositoryContext context)
            : base(context) { }

        public void DeleteCompanyContact(CompanyContact companyContact)
        {
            Delete(companyContact);
        }

    }
}
