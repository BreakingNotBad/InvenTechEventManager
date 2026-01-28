using Contracts.IRepository;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repository.BaseManager;
using Shared.RequestFeatures.Parameters;

namespace Repositories.Repository
{
    public class CompanyRepository : RepositoryBase<Company>, ICompanyRepository
    {
        public CompanyRepository(RepositoryContext context)
            : base(context) { }

        public async Task<IEnumerable<Company>> GetCompaniesAsync(CompanyParameter parameters, bool trackChanges)
        {
            var companies = FindAll(trackChanges);

            // Search CompanyName
            if (!string.IsNullOrWhiteSpace(parameters.CompanyName))
            {
                companies = companies.Where(c => c.CompanyName.ToLower().Contains(parameters.CompanyName.ToLower()));
            }

            // Search CompanyContact (Sub-query logic)
            if (!string.IsNullOrWhiteSpace(parameters.CompanyContact))
            {
                var searchTerm = parameters.CompanyContact.ToLower();
                companies = companies.Where(c =>
                    c.CompanyContacts.Any(cc =>
                        cc.FullName != null && cc.FullName.ToLower().Contains(searchTerm)
                    )
                );
            }

            // Search Address
            if (!string.IsNullOrWhiteSpace(parameters.Address))
            {
                companies = companies.Where(c => c.Address != null && c.Address.ToLower().Contains(parameters.Address.ToLower()));
            }

            // Exact Match Filters
            if (parameters.Latitude.HasValue)
                companies = companies.Where(c => c.Latitude == parameters.Latitude);

            if (parameters.Longitude.HasValue)
                companies = companies.Where(c => c.Longitude == parameters.Longitude);

            if (parameters.IsDeleted.HasValue)
                companies = companies.Where(c => c.IsDeleted == parameters.IsDeleted.Value);

            // Date Filters (ระวังเรื่อง Time component)
            if (parameters.CreatedAt != default(DateTime))
                companies = companies.Where(c => c.CreatedAt.Date == parameters.CreatedAt.Date);

            if (parameters.UpdatedAt != default(DateTime))
                companies = companies.Where(c => c.UpdatedAt.HasValue && c.UpdatedAt.Value.Date == parameters.UpdatedAt.Date);

            // 3. Execute Query และ Return (ใส่ Include ตามเดิม)
            return await companies
                .Include(e => e.CompanyContacts)
                .ToListAsync();
        }

        public async Task<Company?> GetCompanyByIdAsync(int id, bool trackchanges)
        {
            return await FindByCondition(e => e.CompanyId == id, trackchanges)
                .Include(e => e.CompanyContacts)
                .FirstOrDefaultAsync();
        }

        public void CreateCompany(Company company)
        {
            Create(company);
        }

        public void UpdateCompany(Company company)
        {
            Update(company);
        }

        public void DeleteCompany(Company company)
        {
            Delete(company);
        }
    }
}
