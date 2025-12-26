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
                throw new ArgumentException($"Outsource with id {id} not found.");
            }
            _repo.Company.DeleteCompany(exinstingCompany);
            await _repo.SaveAsync();
        }
        public async Task UpdateCompanyAsync(Company company)
        {
            var existingCompany = await _repo.Company.GetCompanyByIdAsync(company.CompanyId);
            if (existingCompany == null)
            {
                throw new ArgumentException($"Company with id {company.CompanyId} not found.");
            }
            // Update fields
            existingCompany.CompanyName = company.CompanyName;
            // Add other fields as necessary
            _repo.Company.UpdateCompany(existingCompany);
            await _repo.SaveAsync();
        }

        public async Task<Company?> GetCompanyContactByCompanyIdAsync(int id)
        {
            return await _repo.Company.GetCompanyContactsAsync(id);
        }
    }
}
