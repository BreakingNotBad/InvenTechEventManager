using AutoMapper;
using Contracts.IRepository.BaseManager;
using Entities.Models;
using Service.Contracts.DTOs.Staff;
using Service.Contracts.IService;

namespace Service.Service
{
    public class StaffService : IStaffService
    {
        private readonly IRepositoryManager _repo;
        private readonly IFileService _fileService;
        private readonly IMapper _mapper;

        public StaffService(IRepositoryManager repo, IFileService fileService, IMapper mapper)
        {
            _repo = repo;
            _fileService = fileService;
            _mapper = mapper;
        }

        // GET ALL
        public async Task<IEnumerable<Staff>> GetStaffMembersAsync(
            string? fullName,
            string? status,
            string? role,
            bool? available,
            DateTime? date,
            string? period
        )
        {
            var staffs = await _repo.Staff.GetStaffMembersAsync();

            //  search
            if (!string.IsNullOrWhiteSpace(fullName))
            {
                staffs = staffs.Where(
                    s => s.FullName?.ToLower().Contains(fullName.Trim().ToLower()) == true 
                );
            }

            //  filter role
            if (!string.IsNullOrWhiteSpace(role))
            {
                staffs = staffs.Where(s =>
                    s.StaffRoles.Any(r => r.Role.RoleName.Equals(role)) 
                );
            }

            //  filter status (active / inactive)
            if (!string.IsNullOrWhiteSpace(status))
            {
                status = status.Trim().ToLower();

                if (status == "active" || status == "false")
                {
                    staffs = staffs.Where(s => !s.IsDeleted);
                }
                else if (status == "inactive" || status == "true")
                {
                    staffs = staffs.Where(s => s.IsDeleted);
                }
            }

            //// filter available
            //if (available.HasValue)
            //{
            //    staffList = staffList.Where(s => s.IsAvailable == available.Value);
            //}

            //  filter date / time_period
            if (date.HasValue && !string.IsNullOrWhiteSpace(period))
            {
                var start = date.Value.Date;
                DateTime end = period.ToLower() switch
                {
                    "day" => start.AddDays(1),
                    "week" => start.AddDays(7),
                    "month" => start.AddMonths(1),
                    _ => start.AddDays(1),
                };

                staffs = staffs.Where(s => s.CreatedAt >= start && s.CreatedAt < end);
            }

            return staffs;
        }

        // GET BY ID
        public async Task<Staff?> GetStaffByIdAsync(int id)
        {
            return await _repo.Staff.GetStaffByIdAsync(id,false);
        }

        // CREATE
        public async Task<Staff> CreateStaffAsync(
            CreateStaffDto staffDto,
            Stream? avatarStream,
            string? avatarFileName
        )
        {
            // 1. จัดการเรื่องไฟล์
            Console.WriteLine($"--------------------------------------------------");
            Console.WriteLine($"[Start] Creating Staff: {staffDto.FullName} (Email: {staffDto.Email})");
            string? avatarPath = null; // กำหนดค่าเริ่มต้นเป็น null

            // เช็คว่ามี Stream ส่งมาไหม
            if (
                avatarStream != null // ถ้ามีไฟล์แนบมา 
                && avatarStream.Length > 0 // และไฟล์ไม่ว่างเปล่า
                && !string.IsNullOrEmpty(avatarFileName) // และมีชื่อไฟล์
            )
            {
                Console.WriteLine($"[File] Uploading avatar: {avatarFileName} ({avatarStream.Length} bytes)...");
                // ให้บันทึกไฟล์ผ่าน FileService
                avatarPath = await _fileService.SaveFileAsync( // เรียกใช้เมธอด SaveFileAsync
                    avatarStream, // ส่ง Stream ของไฟล์
                    avatarFileName, // ชื่อไฟล์
                    "Staff" // โฟลเดอร์ที่เก็บไฟล์ ชื่อโฟลเดอร์ "Staff"
                );
                Console.WriteLine($"[File] Avatar saved at: {avatarPath}");
            }

            // 2. สร้าง Entity (Mapping)
            var staffEntity = _mapper.Map<Staff>(staffDto); // แปลงจาก DTO เป็น Entity
            staffEntity.Avatar = avatarPath; // กำหนดค่า Avatar ที่ได้จากการบันทึกไฟล์ ไปยัง Entity
            // 3. จัดการ Role
            if (staffDto.RoleIds != null)// ถ้ามีการกำหนด RoleIds มา
            {
                Console.WriteLine($"[Role] Assigning {staffDto.RoleIds.Count} roles...");
                staffEntity.StaffRoles = staffDto.RoleIds // รับ RoleIds จาก DTO
                    .Select(roleId => new StaffRole { RoleId = roleId }) // สร้าง StaffRole ใหม่สำหรับแต่ละ RoleId
                    .ToList();
            }
            Console.WriteLine($"[DB] Saving to database...");
            // 4. บันทึกลง Database
            _repo.Staff.CreateStaff(staffEntity);
            await _repo.SaveAsync();
            return staffEntity;
        }

        // UPDATE
        public async Task UpdateStaffAsync(
            int id,
            UpdateStaffDto dto,
            Stream? avatarStream,
            string? avatarFileName
        )
        {
            Console.WriteLine($"--------------------------------------------------");
            Console.WriteLine($"[Start] Updating Staff ID: {id} | Name: {dto.FullName}");
            var staff = await _repo.Staff.GetStaffByIdAsync(id,true);

            if (staff == null)
                throw new KeyNotFoundException($"Staff with id {id} not found.");

            // update fields
            _mapper.Map(dto, staff);
            staff.UpdatedAt = DateTime.UtcNow;

            // Delete Avatar
            if (dto.DeleteAvatar == true) // ถ้ามีการส่งค่านี้มาเป็น true
            {
                if (!string.IsNullOrEmpty(staff.Avatar))// ถ้ามี Avatar เดิมอยู่
                {
                    Console.WriteLine($"[Avatar] Removing old file at: {staff.Avatar}");
                    await _fileService.DeleteFileAsync(staff.Avatar);// ลบไฟล์เก่าออก
                }
                staff.Avatar = null;// ตั้งค่า Avatar ใน DB เป็น null
            }
            // avatar
            else if (avatarStream != null && !string.IsNullOrEmpty(avatarFileName)) // ถ้ามีการส่งไฟล์ใหม่มา
            {
                Console.WriteLine($"[Avatar] New file detected: {avatarFileName} ({avatarStream.Length} bytes)");
                // ลบไฟล์เก่าออกก่อน
                if (!string.IsNullOrEmpty(staff.Avatar))// ถ้ามี Avatar เดิมอยู่
                Console.WriteLine($"[Avatar] Removing old file before replacement...");
                await _fileService.DeleteFileAsync(staff.Avatar);// ลบไฟล์เก่าออก

                // บันทึกไฟล์ใหม่
                var newAvatarPath = await _fileService.SaveFileAsync( // เรียกใช้เมธอด SaveFileAsync
                    avatarStream,
                    avatarFileName,
                    "Staff" // เก็บในโฟลเดอร์ "Staff"
                );
                Console.WriteLine($"[Avatar] New file saved at: {newAvatarPath}");
                // อัปเดตเส้นทางไฟล์ในฐานข้อมูล
                staff.Avatar = newAvatarPath;
            }

            // update roles
            if (dto.RoleIds != null) // ถ้ามีการส่ง RoleIds มา
            {
                Console.WriteLine($"[Role] Updating roles. New Role IDs: [{string.Join(", ", dto.RoleIds)}]");
                staff.StaffRoles.Clear(); // ลบ Role เดิมทั้งหมดออกก่อน
                foreach (var roleId in dto.RoleIds) // เพิ่ม Role ใหม่ตามที่ส่งมา
                {
                    staff.StaffRoles.Add(
                        new StaffRole { StaffId = staff.StaffId, RoleId = roleId } // สร้าง StaffRole ใหม่
                    );
                }
            }

            // เช็คการลบ
            if (dto.IsDeleted.HasValue) // ถ้ามีการส่งค่านี้มา
            {
                Console.WriteLine($"[Status] Changing 'IsDeleted' to: {dto.IsDeleted.Value}");
                staff.IsDeleted = dto.IsDeleted.Value; // อัปเดตสถานะ
            }
            Console.WriteLine($"[DB] Saving changes to database...");
            _repo.Staff.UpdateStaff(staff);
            await _repo.SaveAsync();
            Console.WriteLine($"[Success] Staff ID {id} updated successfully.");
            Console.WriteLine($"--------------------------------------------------");
        }

        // DELETE
        public async Task DeleteStaffAsync(int id)
        {
            var exinstingStaff = await _repo.Staff.GetStaffByIdAsync(id,true);

            if (exinstingStaff == null)
            {
                throw new KeyNotFoundException($"Staff with id: {id} does not exist.");
            }

            _repo.Staff.DeleteStaff(exinstingStaff);
            await _repo.SaveAsync();
        }

        public async Task SoftDeleteStaffAsync(int id, bool isDeleted)
        {
            var existingStaff = await _repo.Staff.GetStaffByIdAsync(id,true);
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
