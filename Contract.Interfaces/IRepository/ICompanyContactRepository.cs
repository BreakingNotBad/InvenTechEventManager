using Entity.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract.Interfaces.IRepository
{
    public interface ICompanyContactRepository
    {
        void DeleteCompanyContact(CompanyContact companyContact);
    }
}
