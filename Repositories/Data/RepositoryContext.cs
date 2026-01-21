using Entities.Interfaces;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repositories.Data
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options) { }

        public DbSet<Event> Events { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<CompanyContact> CompanyContacts { get; set; } = null!;
        public DbSet<Staff> Staff { get; set; } = null!;
        public DbSet<StaffPermission> StaffPermissions { get; set; } = null!;
        public DbSet<Permission> Permissions { get; set; } = null!;
        public DbSet<EventStaff> EventStaff { get; set; } = null!;
        public DbSet<Outsource> Outsources { get; set; } = null!;
        public DbSet<EventOutsource> EventOutsources { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<StaffRole> StaffRoles { get; set; } = null!;
        public DbSet<Equipment> Equipments { get; set; } = null!;
        public DbSet<EquipmentSet> EquipmentSets { get; set; } = null!;
        public DbSet<EventExtraEquipment> EventExtraEquipments { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Package> Packages { get; set; } = null!;
        public DbSet<EventAttachment> EventAttachments { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // แปลง Enum เป็น String (Morning, Afternoon)
            modelBuilder.Entity<Event>().Property(x => x.Period).HasConversion<string>();

            // Composite Keys
            modelBuilder.Entity<EquipmentSet>().HasKey(x => new { x.PackageId, x.EquipmentId });

            modelBuilder
                .Entity<EventExtraEquipment>()
                .HasKey(x => new { x.EventId, x.EquipmentId });

            modelBuilder.Entity<EventOutsource>(entity =>
            {
                // ✅ Key มีแค่ 2 ตัว (Event + Outsource)
                // ผลลัพธ์: นาย ก. ใน งาน A จะมีได้แค่ "บรรทัดเดียว"
                // พอมีบรรทัดเดียว ก็แปลว่าใส่ Role ได้แค่ช่องเดียว (1 Role) ครับ
                entity.HasKey(x => new { x.EventId, x.OutsourceId });

                // FK Config
                entity
                    .HasOne(x => x.Event)
                    .WithMany(e => e.EventOutsources)
                    .HasForeignKey(x => x.EventId);

                entity
                    .HasOne(x => x.Outsource)
                    .WithMany(o => o.EventOutsources)
                    .HasForeignKey(x => x.OutsourceId);

                // เชื่อม Role แบบ One-to-Many ปกติ
                entity
                    .HasOne(x => x.Role)
                    .WithMany(r => r.EventOutsources)
                    .HasForeignKey(x => x.RoleId)
                    .OnDelete(DeleteBehavior.Restrict); // แนะนำ Restrict: ห้ามลบ Role ถ้ามีคนใช้อยู่
            });

            modelBuilder.Entity<StaffPermission>().HasKey(x => new { x.StaffId, x.PermissionId });

            modelBuilder.Entity<StaffRole>().HasKey(x => new { x.StaffId, x.RoleId });

            modelBuilder.Entity<EventStaff>(entity =>
            {
                // Composite Key
                entity.HasKey(x => new { x.EventId, x.StaffId });

                // ความสัมพันธ์ฝั่ง Event
                entity
                    .HasOne(x => x.Event)
                    .WithMany(e => e.EventStaff)
                    .HasForeignKey(x => x.EventId)
                    .OnDelete(DeleteBehavior.Cascade); // "ลบ Event ทิ้ง" -> ข้อมูลในตาราง EventStaff ที่เกี่ยวกับงานนี้จะหายไปทันที

                // ความสัมพันธ์ฝั่ง Staff
                entity
                    .HasOne(x => x.Staff)
                    .WithMany(s => s.EventStaff)
                    .HasForeignKey(x => x.StaffId)
                    .OnDelete(DeleteBehavior.NoAction); // "ลบ Staff ทิ้ง" (โดยที่เขายังมีชื่อผูกอยู่ในงาน) -> Database จะ Error (ห้ามลบ)
            });

            // Event นี้ ใครเป็นคนกดสร้าง
            modelBuilder
                .Entity<Event>()
                .HasOne(e => e.CreatedByStaff)
                .WithMany(s => s.CreatedEvents)
                .HasForeignKey(e => e.CreatedByStaffId)
                .OnDelete(DeleteBehavior.NoAction);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // วนลูปดูว่ามี Entity ตัวไหนบ้างที่มีการเปลี่ยนแปลง (Added หรือ Modified)
            // และ Entity ตัวนั้นต้องสืบทอด IAuditable ด้วยนะ
            var entries = ChangeTracker
                .Entries<IAuditable>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                // ใช้ UtcNow เพื่อป้องกันปัญหาเรื่อง Timezone ของ Server
                var now = DateTime.UtcNow;

                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                    // ตอนสร้างใหม่ UpdatedAt ควรเป็น null หรือจะให้เท่ากับ CreatedAt ก็ได้แล้วแต่ design
                    entry.Entity.UpdatedAt = null;
                }

                if (entry.State == EntityState.Modified)
                {
                    // ถ้าเป็นการแก้ไข ให้แก้แค่ UpdatedAt
                    entry.Entity.UpdatedAt = now;

                    // ป้องกันไม่ให้ CreatedAt โดนแก้ (เผื่อมีใครเผลอไป set ค่าใหม่)
                    entry.Property(x => x.CreatedAt).IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
