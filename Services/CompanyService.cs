using Contract.Interfaces.IRepository;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Service.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repo;

        public CompanyService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync()
        {
            return await _repo.Company.GetCompaniesAsync();
        }

        public async Task<Company?> GetCompanyByIdAsync(int id)
        {
            var query = _repo.Company.FindByCondition(
                e => e.CompanyId == id,
                trackChanges: false
            );

            return await query.FirstOrDefaultAsync();
        }
    }
}
