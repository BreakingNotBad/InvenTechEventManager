using Contracts.DTOs;
using Contracts.IRepository.BaseManager;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;

namespace Service
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repo;

        public CompanyService(IRepositoryManager repo)
        {
            _repo = repo;
        }

        // GET ALL
        public async Task<IEnumerable<Company>> GetCompaniesAsync(
            string? companyName,
            string? companyContact
        )
        {
            var companiesList = await _repo.Company.GetCompaniesAsync();

            // 🔍 search (case-insensitive)
            if (!string.IsNullOrWhiteSpace(companyName))
            {
                companiesList = companiesList.Where(c =>
                    (
                        !string.IsNullOrEmpty(c.CompanyName)
                        && c.CompanyName.Contains(companyName, StringComparison.OrdinalIgnoreCase)
                    )
                    || (
                        c.CompanyContacts != null
                        && c.CompanyContacts.Any(cc =>
                            (
                                !string.IsNullOrEmpty(cc.FullName)
                                && cc.FullName.Contains(
                                    companyName,
                                    StringComparison.OrdinalIgnoreCase
                                )
                            )
                            || (
                                !string.IsNullOrEmpty(cc.Email)
                                && cc.Email.Contains(
                                    companyName,
                                    StringComparison.OrdinalIgnoreCase
                                )
                            )
                            || (
                                !string.IsNullOrEmpty(cc.PhoneNumber)
                                && cc.PhoneNumber.Contains(
                                    companyName,
                                    StringComparison.OrdinalIgnoreCase
                                )
                            )
                        )
                    )
                );
            }

            if (!string.IsNullOrWhiteSpace(companyContact))
            {
                companiesList = companiesList.Where(c =>
                    c.CompanyContacts != null
                    && c.CompanyContacts.Any(cc =>
                        cc.FullName.Contains(companyContact, StringComparison.OrdinalIgnoreCase)
                        || cc.Email.Contains(companyContact, StringComparison.OrdinalIgnoreCase)
                        || cc.PhoneNumber.Contains(
                            companyContact,
                            StringComparison.OrdinalIgnoreCase
                        )
                    )
                );
            }

            return companiesList;
        }

        // GET BY ID
        public async Task<Company?> GetCompanyByIdAsync(int id)
        {
            return await _repo.Company.GetCompanyByIdAsync(id);
        }

        // CREATE
        public async Task<Company> CreateCompanyAsync(CreateCompanyDto dto)
        {
            var company = new Company
            {
                CompanyName = dto.CompanyName,
                Address = dto.Address,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
                CompanyContacts =
                    dto.CompanyContacts?.Select(cc => new CompanyContact
                        {
                            FullName = cc.FullName,
                            Position = cc.Position,
                            Email = cc.Email,
                            PhoneNumber = cc.PhoneNumber,
                            IsPrimary = cc.IsPrimary,
                            IsDeleted = false,
                        })
                        .ToList()
                    ?? new List<CompanyContact>(),
            };

            _repo.Company.CreateCompany(company);
            await _repo.SaveAsync();

            return company;
        }

        // UPDATE
        public async Task<Company> UpdateCompanyAsync(int id, UpdateCompanyDto dto)
        {
            var company = await _repo
                .Company.FindByCondition(c => c.CompanyId == id, trackChanges: true)
                .Include(c => c.CompanyContacts)
                .FirstOrDefaultAsync();

            if (company == null)
                throw new KeyNotFoundException($"Company with id {id} not found.");

            // update company
            company.CompanyName = dto.CompanyName;
            company.Address = dto.Address;
            company.Latitude = dto.Latitude;
            company.Longitude = dto.Longitude;
            company.IsDeleted = dto.IsDeleted;

            //  DELETE
            if (dto.CompanyContacts != null)
            {
                // ID ที่ frontend ส่งมา
                var requestContactIds = dto
                    .CompanyContacts.Where(x => x.CompanyContactId.HasValue)
                    .Select(x => x.CompanyContactId!.Value)
                    .ToList();

                // ตัวที่อยู่ใน DB แต่ไม่มีใน request  ต้องลบ
                var contactsToDelete = company
                    .CompanyContacts.Where(existing =>
                        !requestContactIds.Contains(existing.CompanyContactId)
                    )
                    .ToList();

                foreach (var contact in contactsToDelete)
                {
                    _repo.CompanyContact.DeleteCompanyContact(contact);
                }
            }
            // update contacts
            if (dto.CompanyContacts != null)
            {
                foreach (var contactDto in dto.CompanyContacts)
                {
                    if (contactDto.CompanyContactId.HasValue)
                    {
                        var contact = company.CompanyContacts.FirstOrDefault(c =>
                            c.CompanyContactId == contactDto.CompanyContactId
                        );

                        if (contact == null)
                            continue;

                        contact.FullName = contactDto.FullName;
                        contact.Position = contactDto.Position;
                        contact.Email = contactDto.Email;
                        contact.PhoneNumber = contactDto.PhoneNumber;
                        contact.IsPrimary = contactDto.IsPrimary;
                        contact.IsDeleted = contactDto.IsDeleted;
                    }
                    else
                    {
                        company.CompanyContacts.Add(
                            new CompanyContact
                            {
                                FullName = contactDto.FullName,
                                Position = contactDto.Position,
                                Email = contactDto.Email,
                                PhoneNumber = contactDto.PhoneNumber,
                                IsPrimary = contactDto.IsPrimary,
                                IsDeleted = contactDto.IsDeleted,
                            }
                        );
                    }
                }
            }

            await _repo.SaveAsync();

            return company;
        }

        // DELETE
        public async Task DeleteCompanyAsync(int id)
        {
            var exinstingCompany = await _repo.Company.GetCompanyByIdAsync(id);

            if (exinstingCompany == null)
            {
                throw new KeyNotFoundException($"Company with id {id} not found.");
            }

            _repo.Company.DeleteCompany(exinstingCompany);
            await _repo.SaveAsync();
        }
    }
}
