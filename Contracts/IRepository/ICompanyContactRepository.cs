using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts.IRepository
{
    public interface ICompanyContactRepository
    {
        void DeleteCompanyContact(CompanyContact companyContact);
    }
}
