using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contract.Interfaces.IRepository.BaseManager;
using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Service.Contract;

namespace Service
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repo;

        public CompanyService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync(string? companyName)
        {
            var companiesList = await _repo.Company.GetCompaniesAsync();

            // 🔍 search (case-insensitive)
            if (!string.IsNullOrWhiteSpace(companyName))
            {
                companiesList = companiesList.Where(c =>
                    c.CompanyName.Contains(companyName, StringComparison.OrdinalIgnoreCase)
                );
            }

            return companiesList;
        }

        public async Task<Company?> GetCompanyByIdAsync(int id)
        {
            return await _repo.Company.GetCompanyByIdAsync(id);
        }

        public async Task CreateCompanyAsync(Company company)
        {
            _repo.Company.CreateCompany(company);
            await _repo.SaveAsync();
        }

        public async Task DeleteCompanyAsync(int id)
        {
            var exinstingCompany = await _repo.Company.GetCompanyByIdAsync(id);
            if (exinstingCompany == null)
            {
                throw new KeyNotFoundException($"Outsource with id {id} not found.");
            }
            _repo.Company.DeleteCompany(exinstingCompany);
            await _repo.SaveAsync();
        }

        public async Task UpdateCompanyAsync(int id, Company company)
        {
            var existingCompany = await _repo.Company.GetCompanyByIdAsync(id);
            if (existingCompany == null)
            {
                throw new KeyNotFoundException($"Company with id {id} not found.");
            }

            // Update fields
            existingCompany.CompanyName = company.CompanyName;

            _repo.Company.UpdateCompany(existingCompany);
            await _repo.SaveAsync();
        }

        public async Task<Company?> GetCompanyContactByCompanyIdAsync(int id)
        {
            return await _repo.Company.GetCompanyContactsAsync(id);
        }
    }
}
