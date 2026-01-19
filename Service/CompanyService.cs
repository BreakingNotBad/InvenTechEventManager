using System.Text.Json;
using Contracts.DTOs;
using Contracts.IRepository.BaseManager;
using Entities.Models;
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
            string? companyContact,
            string? Address,
            decimal? Latitude,
            decimal? Longitude,
            bool IsDeleted,
            DateTime CreatedAt,
            DateTime UpdatedAt
        )
        {
            var companiesList = await _repo.Company.GetCompaniesAsync();

            // search companyName (ถ้ามีค่าชื่อ company ที่ผู้ใช้กรอกมาจริงๆ ไม่ใช่ null กับ ปล่อยว่าง จะผ่านเงื่อนไข เพื่อไปค้นหา companyName)
            if (!string.IsNullOrWhiteSpace(companyName))
            {
                companiesList = companiesList.Where(c => // where companyName เพื่อกรองข้อมูลที่มีค่า companyName ที่เป็น null ออกไปก่อน
                    c.CompanyName.ToLower().Contains(companyName.ToLower()) // และมาค้นหาชื่อ companyName ที่ตรงกับค่าที่ผู้ใช้กรอกเข้ามา (ไม่สนใจตัวพิมพ์เล็กพิมพ์ใหญ่)
                ); 
            }

            // search companyContact (FullName, Email, PhoneNumber)
            if (!string.IsNullOrWhiteSpace(companyContact)) // ถ้ามีค่าชื่อ contact ที่ผู้ใช้กรอกมาจริงๆ ไม่ใช่ null กับ ปล่อยว่าง จะผ่านเงื่อนไข เพื่อไปค้นหา companyContact
            {
                companiesList = companiesList.Where(c => // where companyContact เพื่อกรองข้อมูลที่มีค่า companyContact ที่เป็น null ออกไปก่อน
                    c.CompanyContacts.Any(cc => // ตรวจสอบในตาราง CompanyContacts ว่ามีข้อมูลที่ตรงกับค่าที่ผู้ใช้กรอกเข้ามาหรือไม่
                        (
                            cc.FullName != null
                            && // ตรวจสอบ FullName ไม่เป็น null
                            cc.FullName.ToLower().Contains(companyContact.ToLower())
                        ) //และมาค้นหาชื่อ FullName ที่ตรงกับค่าที่ผู้ใช้กรอกเข้ามา (ไม่สนใจตัวพิมพ์เล็กพิมพ์ใหญ่)
                    )
                );
            }

            // search Address
            if (!string.IsNullOrWhiteSpace(Address))
            {
                companiesList = companiesList.Where(c =>
                    c.Address != null && c.Address.ToLower().Contains(Address.ToLower())
                );
            }

            // search Latitude
            if (Latitude.HasValue)
            {
                companiesList = companiesList.Where(c => c.Latitude == Latitude);
            }

            // search Longitude
            if (Longitude.HasValue)
            {
                companiesList = companiesList.Where(c => c.Longitude == Longitude);
            }

            // search IsDeleted
            companiesList = companiesList.Where(c => c.IsDeleted == IsDeleted);

            // search CreatedAt
            if (CreatedAt != default(DateTime))
            {
                companiesList = companiesList.Where(c => c.CreatedAt.Date == CreatedAt.Date);
            }

            // search UpdatedAt
            if (UpdatedAt != default(DateTime))
            {
                companiesList = companiesList.Where(c =>
                    c.UpdatedAt.HasValue && c.UpdatedAt.Value.Date == UpdatedAt.Date
                );
            }

            return companiesList;
        }

        // GET BY ID
        public async Task<Company?> GetCompanyByIdAsync(int id)
        {
            return await _repo.Company.GetCompanyByIdAsync(id, false);
        }

        // CREATE
        public async Task<Company> CreateCompanyAsync(CreateCompanyDto dto) // รับค่ามาจาก controller ผ่าน dto
        {
            // ไว้ทำ validation เองทีหลัง (เดี๋ยวมาลบด้วย)
            if (string.IsNullOrWhiteSpace(dto.CompanyName)) // ตรวจสอบว่ามีการกรอก CompanyName มาหรือไม่
                throw new Exception("CompanyName is required.");

            // เดี๋ยวทำ autp mapper (เดี๋ยวมาลบด้วย)
            var company = new Company // สร้าง object company ใหม่
            {
                CompanyName = dto.CompanyName,
                Address = dto.Address,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                IsDeleted = false,
                CreatedAt = DateTime.UtcNow,
            };

            // ไว้ทำ validation เองทีหลัง (เดี๋ยวมาลบด้วย)
            if (dto.CompanyContacts.Any()) // ตรวจสอบว่ามีการกรอก CompanyContacts มาหรือไม่
            {
                company.CompanyContacts = dto // เดี๋ยวทำ autp mapper (เดี๋ยวมาลบด้วย)
                    .CompanyContacts.Select(c => new CompanyContact // สร้าง object CompanyContact ใหม่ 
                    {
                        FullName = c.FullName,
                        Position = c.Position,
                        Email = c.Email,
                        PhoneNumber = c.PhoneNumber,
                        IsPrimary = c.IsPrimary,
                        IsDeleted = false,
                    })
                    .ToList();

                // ไว้ทำ validation เองทีหลัง (เดี๋ยวมาลบด้วย)
                if (company.CompanyContacts.Count(cc => cc.IsPrimary) > 1) // ตรวจสอบว่ามีการกำหนด IsPrimary มากกว่า 1 รายการหรือไม่
                    throw new Exception("Company can have only one primary contact.");
            }

            _repo.Company.CreateCompany(company);
            await _repo.SaveAsync();
            return company;
        }

        // UPDATE
        public async Task<Company> UpdateCompanyAsync(int id, UpdateCompanyDto companyDto)
        {
            var existingCompany = await _repo.Company.GetCompanyByIdAsync(id, true);

            if (existingCompany == null)
                throw new KeyNotFoundException($"Company with id {id} not found.");

            // update company fields // เดี๋ยวทำ autp mapper (เดี๋ยวมาลบด้วย)
            existingCompany.CompanyName = companyDto.CompanyName;
            existingCompany.Address = companyDto.Address;
            existingCompany.Latitude = companyDto.Latitude;
            existingCompany.Longitude = companyDto.Longitude;
            existingCompany.IsDeleted = companyDto.IsDeleted;

            //  DELETE (CompanyContacts)
            if (companyDto.CompanyContacts != null) // ถ้ามีการส่ง CompanyContacts มา
            {
                // ดึงเฉพาะ CompanyContactId ที่มีค่า จาก request
                // เช่น FE ส่ง [1,3,null] มา (null คือรายการใหม่ที่เพิ่มเข้ามา)
                // เอา CompanyContactId ที่มีค่าไปเก็บไว้ เพื่อใช้ตรวจสอบว่ารายการไหนต้องลบ
                // ส่วน null จะข้ามไป
                var requestContactIds = companyDto
                    .CompanyContacts.Where(x => x.CompanyContactId.HasValue) // เลือกเฉพาะรายการที่มี CompanyContact [1,3]
                    .Select(x => x.CompanyContactId!.Value)
                    .ToList();

                // หา CompanyContact ที่มีอยู่ใน database แต่ไม่มีใน request
                // เช่น ใน Database มี [1,2,3] แต่ใน requestContactIds มี [1,3] จะได้รายการที่ต้องลบคือ [2]
                // เพราะ id 2 ไม่มีใน request
                // เราจะลบรายการที่ไม่มีใน request ออก
                var contactsToDelete = existingCompany
                    .CompanyContacts.Where(contact =>
                    {
                        var existsInRequest = requestContactIds.Contains(contact.CompanyContactId); // requestContactIds ที่รับมา [1,3] แล้วใน DB มี 1,2,3 
                        // หมายถึง [1,3] มี 1 อยู่ไหม? มี = true, [1,3] มี 2 อยู่ไหม? ไม่มี = false, [1,3] มี 3 อยู่ไหม? มี = true
                        // จะได้ค่า existsInRequest = true,false,true
                        return !existsInRequest; // เจอ ! เปลี่ยนจาก true,false,true เป็น false,true,false และ return ค่าที่เป็น true ออกมา คือ id 2 และนำไปเก็บไว้ที่ contactsToDelete
                    })
                    .ToList();

                // ลบรายการที่ต้องการลบ
                foreach (var contact in contactsToDelete)
                {
                    _repo.CompanyContact.DeleteCompanyContact(contact);
                }
            }

            // update contacts and add new contacts
            if (companyDto.CompanyContacts != null) // ถ้ามีการส่ง CompanyContacts มา
            {
                foreach (var contactDto in companyDto.CompanyContacts) // วนลูปรายการ CompanyContacts ที่ส่งมาใน request
                {
                    if (contactDto.CompanyContactId.HasValue) // ถ้ามี CompanyContactId แสดงว่าเป็นการอัพเดทข้อมูล
                    {
                        var contact = existingCompany.CompanyContacts.FirstOrDefault(c => // หา CompanyContact ที่ตรงกับ CompanyContactId
                            c.CompanyContactId == contactDto.CompanyContactId // ใช้ CompanyContactId จาก request มาเทียบกับข้อมูลที่มีอยู่ใน database
                        );

                        if (contact == null) // ถ้าไม่พบ contact ที่จะอัพเดท ให้ข้ามไปยังรายการถัดไป
                            continue;

                        contact.FullName = contactDto.FullName;
                        contact.Position = contactDto.Position;
                        contact.Email = contactDto.Email;
                        contact.PhoneNumber = contactDto.PhoneNumber;
                        contact.IsPrimary = contactDto.IsPrimary;
                        contact.IsDeleted = contactDto.IsDeleted;
                    }
                    else // ถ้าไม่มี CompanyContactId แสดงว่าเป็นการเพิ่มข้อมูลใหม่
                    {
                        existingCompany.CompanyContacts.Add(
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

                    // ตรวจสอบว่ามีการกำหนด IsPrimary มากกว่า 1 รายการหรือไม่
                    if (existingCompany.CompanyContacts.Count(c => c.IsPrimary) > 1)
                        throw new Exception("Company can have only one primary contact.");
                }
            }

            await _repo.SaveAsync();
            return existingCompany;
        }

        // DELETE
        public async Task DeleteCompanyAsync(int id)
        {
            var exinstingCompany = await _repo.Company.GetCompanyByIdAsync(id, true);

            if (exinstingCompany == null)
            {
                throw new KeyNotFoundException($"Company with id {id} not found.");
            }

            _repo.Company.DeleteCompany(exinstingCompany);
            await _repo.SaveAsync();
        }
    }
}
