using AutoMapper;
using Contracts.IRepository.BaseManager;
using Entities.Exceptions;
using Entities.Models;
using FluentValidation;
using Service.Contracts.DTOs.Staff;
using Service.Contracts.IService;
using Shared.RequestFeatures.Parameters;
using System.Security.Cryptography;

namespace Service.Service
{
    public class StaffService : IStaffService
    {
        private readonly IRepositoryManager _repo;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IValidator<CreateStaffDto> _createValidator;
        private readonly IValidator<UpdateStaffDto> _updateValidator;
        private readonly IEmailService _emailService;

        public StaffService(
            IRepositoryManager repo,
            IFileService fileService,
            IMapper mapper,
            IValidator<CreateStaffDto> createValidator,
            IValidator<UpdateStaffDto> updateValidator,
            IEmailService emailService
        )
        {
            _repo = repo;
            _fileService = fileService;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _emailService = emailService;
        }

        // GET ALL
        public async Task<IEnumerable<StaffDto>> GetStaffMembersAsync(StaffParameter staffParameter)
        {
            var staffList = await _repo.Staff.GetStaffMembersAsync(
                staffParameter,
                trackChanges: false
            );

            // แปลงข้อมูลจาก Entity เป็น Dto
            var staffResponse = _mapper.Map<IEnumerable<StaffDto>>(staffList);

            // ถ้ามีการส่ง Date/Period มา แสดงว่าต้องใส่ Status ("Available", "WorkingToday", "Unavailable")
            if (staffParameter.Date.HasValue)
            {
                var checkPeriod = staffParameter.Period.HasValue;

                foreach (var staff in staffResponse)
                {
                    // หา Entity เพื่อเอาข้อมูล EventStaff ที่ Repo ดึงมาให้
                    var staffEntity = staffList.First(s => s.StaffId == staff.StaffId);

                    // ดึง Event ที่พนักงานคนนี้ทำงานอยู่
                    var workingEvents =
                        staffEntity.EventStaff?.Select(es => es.Event).ToList() ?? [];

                    bool isUnavailable = false;
                    bool hasJobToday = workingEvents.Count > 0;


                    if (hasJobToday && checkPeriod)
                    {
                        // เช็คว่าชนช่วงเวลาไหม
                        isUnavailable = workingEvents.Any(e => e.Period == staffParameter.Period!.Value);
                    }

                    // ใส่ Status
                    if (isUnavailable)
                    {
                        staff.Status = "Unavailable"; // ไม่ว่าง: มีงานชนช่วง Morning/Afternoon ที่เลือก
                    }
                    else if (hasJobToday)
                    {
                        staff.Status = "Working"; // มีงานวันนี้: แต่ว่างในช่วงเวลาที่เลือก
                    }
                    else
                    {
                        staff.Status = "Available"; // ว่าง: วันนี้ไม่มีงานที่ถูกมอบหมายเลย
                    }
                }
            }

            return staffResponse;
        }

        // GET BY ID
        public async Task<StaffDto?> GetStaffByIdAsync(int id)
        {
            var existingStaff = await _repo.Staff.GetStaffByIdAsync(id, false);

            if (existingStaff == null)
            {
                throw new NotFoundException(nameof(Staff), id);
            }

            // แปลงข้อมูลจาก Entity เป็น Dto
            var staffResponse = _mapper.Map<StaffDto>(existingStaff);

            return staffResponse;
        }

        // CREATE
        public async Task<StaffDto> CreateStaffAsync(CreateStaffDto staffDto)
        {
            await _createValidator.ValidateAndThrowAsync(staffDto);

            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            // แปลงข้อมูลจาก Dto เป็น Entity
            var newStaff = _mapper.Map<Staff>(staffDto);

            // เอา Path จาก Dto ใส่ Entity
            newStaff.Avatar = staffDto.Avatar;
            newStaff.Password = null;
            newStaff.PasswordResetToken = token;
            newStaff.PasswordResetTokenExpire =
                DateTime.UtcNow.AddHours(24);

            // จัดการ Role
            if (staffDto.StaffRoles != null) // ถ้ามีการกำหนด RoleIds มา
            {
                newStaff.StaffRoles = staffDto
                    .StaffRoles // รับ RoleIds จาก DTO
                    .Select(roleId => new StaffRole { RoleId = roleId }) // สร้าง StaffRole ใหม่สำหรับแต่ละ RoleId
                    .ToList();
            }

            _repo.Staff.CreateStaff(newStaff);
            await _repo.SaveAsync();

            var safeToken = Uri.EscapeDataString(token);
            var link =
              $"http://localhost:5173/auth/set-password?token={safeToken}";
            await _emailService.SendAsync(
                newStaff.Email,
                "Set your password",
                $"""
                Hello {newStaff.FullName}

                Your account has been created.

                Please click the link below to set your password:
                {link}

                This link will expire in 24 hours.
                """
            );


            var staff = await GetStaffByIdAsync(newStaff.StaffId); // ดึงข้อมูลใหม่ที่ถูกสร้างขึ้นมา

            return staff!;
        }
            
        // UPDATE
        public async Task UpdateStaffAsync(int id, UpdateStaffDto staffDto)
        {
            await _updateValidator.ValidateAndThrowAsync(staffDto);

            var existingStaff = await _repo.Staff.GetStaffByIdAsync(id, true);

            if (existingStaff == null)
            {
                throw new NotFoundException(nameof(Staff), id);
            }

            string? oldAvatar = existingStaff.Avatar;

            // แปลงข้อมูลจาก Entity เป็น Dto
            _mapper.Map(staffDto, existingStaff);

            // สั่งลบรูป (DeleteAvatar = true)
            if (staffDto.DeleteAvatar == true)
            {
                // เช็คจาก oldAvatar ที่เราเก็บไว้ตอนแรก
                if (!string.IsNullOrEmpty(oldAvatar))
                {
                    await _fileService.DeleteFileAsync(oldAvatar);
                }

                // สั่งให้ Entity เป็น null (ไม่ใช่ DTO)
                existingStaff.Avatar = null;
            }

            // มีรูปใหม่มาแทน (staffDto.Avatar มีค่า path ใหม่มา)
            if (staffDto.Avatar != null)
            {
                existingStaff.Avatar = staffDto.Avatar;
            }

            // Update Roles
            if (staffDto.StaffRoles != null)
            {
                existingStaff.StaffRoles.Clear(); // ลบของเก่า
                foreach (var roleId in staffDto.StaffRoles)
                {
                    existingStaff.StaffRoles.Add(
                        new StaffRole { StaffId = existingStaff.StaffId, RoleId = roleId }
                    );
                }
            }

            // เช็คการลบ (Soft Delete)
            if (staffDto.IsDeleted.HasValue)
            {
                existingStaff.IsDeleted = staffDto.IsDeleted.Value; // อัปเดตสถานะ
            }

            _repo.Staff.UpdateStaff(existingStaff);
            await _repo.SaveAsync();
        }

        // DELETE
        public async Task DeleteStaffAsync(int id)
        {
            var exinstingStaff = await _repo.Staff.GetStaffByIdAsync(id, true);

            if (exinstingStaff == null)
            {
                throw new KeyNotFoundException($"Staff with id: {id} does not exist.");
            }

            _repo.Staff.DeleteStaff(exinstingStaff);
            await _repo.SaveAsync();
        }

        public async Task SoftDeleteStaffAsync(int id, bool isDeleted)
        {
            var existingStaff = await _repo.Staff.GetStaffByIdAsync(id, true);
            if (existingStaff == null)
            {
                throw new KeyNotFoundException($"Staff with id {id} not found.");
            }
            existingStaff.IsDeleted = isDeleted;
            _repo.Staff.UpdateStaff(existingStaff);
            await _repo.SaveAsync();
        }
    }
}
