using AutoMapper;
using Contracts.IRepository.BaseManager;
using Entities.Exceptions;
using Entities.Models;
using FluentValidation;
using Service.Contracts.DTOs.Company;
using Service.Contracts.IService;

namespace Service.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly IRepositoryManager _repo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCompanyDto> _createValidator;
        private readonly IValidator<UpdateCompanyDto> _updateValidator;

        public CompanyService(
            IRepositoryManager repo,
            IMapper mapper,
            IValidator<CreateCompanyDto> createValidator,
            IValidator<UpdateCompanyDto> updateValidator
        )
        {
            _repo = repo;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        // GET ALL
        public async Task<IEnumerable<CompanyDto>> GetCompaniesAsync(
            string? companyName,
            string? companyContact,
            string? Address,
            decimal? Latitude,
            decimal? Longitude,
            bool? IsDeleted,
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

            //search Latitude
            if (Latitude.HasValue)
            {
                companiesList = companiesList.Where(c => c.Latitude == Latitude);
            }

            //search Longitude
            if (Longitude.HasValue)
            {
                companiesList = companiesList.Where(c => c.Longitude == Longitude);
            }

            // search IsDeleted
            if (IsDeleted.HasValue)
            {
                companiesList = companiesList.Where(c => c.IsDeleted == IsDeleted.Value);
            }

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

            // แปลงข้อมูลจาก Entity เป็น Dto
            var companyResponse = _mapper.Map<IEnumerable<CompanyDto>>(companiesList);

            return companyResponse;
        }

        // GET BY ID
        public async Task<CompanyDto?> GetCompanyByIdAsync(int id)
        {
            var existingCompany = await _repo.Company.GetCompanyByIdAsync(id, false);

            if (existingCompany == null)
            {
                throw new NotFoundException(nameof(Company), id);
            }

            // แปลงข้อมูลจาก Entity เป็น Dto
            var companyResponse = _mapper.Map<CompanyDto>(existingCompany);

            return companyResponse;
        }

        // CREATE
        public async Task<CompanyDto> CreateCompanyAsync(CreateCompanyDto companyDto)
        {
            await _createValidator.ValidateAndThrowAsync(companyDto);

            // แปลงข้อมูลจาก Dto เป็น Entity
            var newCompany = _mapper.Map<Company>(companyDto);

            _repo.Company.CreateCompany(newCompany);
            await _repo.SaveAsync();

            // แปลงข้อมูลจาก Entity เป็น Dto
            var companyResponse = _mapper.Map<CompanyDto>(newCompany);

            return companyResponse;
        }

        // UPDATE
        public async Task<CompanyDto> UpdateCompanyAsync(int id, UpdateCompanyDto companyDto)
        {
            await _updateValidator.ValidateAndThrowAsync(companyDto);

            var existingCompany = await _repo.Company.GetCompanyByIdAsync(id, true);

            if (existingCompany == null)
            {
                throw new NotFoundException(nameof(Company), id);
            }

            // แปลงข้อมูลจาก Entity เป็น Dto
            _mapper.Map(companyDto, existingCompany);

            //  DELETE (CompanyContacts)
            if (companyDto.CompanyContacts != null) // ถ้ามีการส่ง CompanyContacts มา
            {
                // ดึงเฉพาะ CompanyContactId ที่มีค่า จาก request
                // เช่น FE ส่ง [1,3,null] มา (null คือรายการใหม่ที่เพิ่มเข้ามา)
                // เอา CompanyContactId ที่มีค่าไปเก็บไว้ เพื่อใช้ตรวจสอบว่ารายการไหนต้องลบ
                // ส่วน null จะข้ามไป
                var incomingContactIds = companyDto
                    .CompanyContacts.Where(cc => cc.CompanyContactId.HasValue) // เลือกเฉพาะรายการที่มี CompanyContact [1,3]
                    .Select(cc => cc.CompanyContactId!.Value)
                    .ToList();

                // หา CompanyContact ที่มีอยู่ใน database แต่ไม่มีใน request
                // เช่น ใน Database มี [1,2,3] แต่ใน requestContactIds มี [1,3] จะได้รายการที่ต้องลบคือ [2]
                // เพราะ id 2 ไม่มีใน request
                // เราจะลบรายการที่ไม่มีใน request ออก
                var deletedContacts = existingCompany
                    .CompanyContacts.Where(cc =>
                    {
                        var existsInRequest = incomingContactIds.Contains(cc.CompanyContactId); // requestContactIds ที่รับมา [1,3] แล้วใน DB มี 1,2,3
                        // หมายถึง [1,3] มี 1 อยู่ไหม? มี = true, [1,3] มี 2 อยู่ไหม? ไม่มี = false, [1,3] มี 3 อยู่ไหม? มี = true
                        // จะได้ค่า existsInRequest = true,false,true
                        return !existsInRequest; // เจอ ! เปลี่ยนจาก true,false,true เป็น false,true,false และ return ค่าที่เป็น true ออกมา คือ id 2 และนำไปเก็บไว้ที่ deletedContacts
                    })
                    .ToList();

                // ลบรายการที่ต้องการลบ
                foreach (var contact in deletedContacts)
                {
                    _repo.CompanyContact.DeleteCompanyContact(contact);
                    existingCompany.CompanyContacts.Remove(contact);
                }

                // วนลูปเพื่อ "แก้ไข" หรือ "เพิ่มใหม่" ของ CompanyContacts
                foreach (var contactDto in companyDto.CompanyContacts) // วนลูปรายการ CompanyContacts ที่ส่งมาใน request
                {
                    if (contactDto.CompanyContactId.HasValue) // ถ้ามี CompanyContactId แสดงว่าเป็นการอัพเดทข้อมูล
                    {
                        // -- UPDATE --
                        var existingContact = existingCompany.CompanyContacts.FirstOrDefault(c => // หา CompanyContact ที่ตรงกับ CompanyContactId
                            c.CompanyContactId == contactDto.CompanyContactId // ใช้ CompanyContactId จาก request มาเทียบกับข้อมูลที่มีอยู่ใน database
                        );

                        if (existingContact != null)
                        {
                            _mapper.Map(contactDto, existingContact);
                        }
                    }
                    else // ถ้าไม่มี CompanyContactId แสดงว่าเป็นการเพิ่มข้อมูลใหม่
                    {
                        // -- CREATE --
                        var newContact = _mapper.Map<CompanyContact>(contactDto);
                        existingCompany.CompanyContacts.Add(newContact);
                    }
                }
            }

            await _repo.SaveAsync();

            var companyResponse = _mapper.Map<CompanyDto>(existingCompany); // แปลงข้อมูลจาก Entity เป็น Dto

            return companyResponse;
        }

        // DELETE
        public async Task DeleteCompanyAsync(int id)
        {
            var existingCompany = await _repo.Company.GetCompanyByIdAsync(id, true);

            if (existingCompany == null)
            {
                throw new NotFoundException(nameof(Company), id);
            }

            _repo.Company.DeleteCompany(existingCompany);
            await _repo.SaveAsync();
        }
    }
}
