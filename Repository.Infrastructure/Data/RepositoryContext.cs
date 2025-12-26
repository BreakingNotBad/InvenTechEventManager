using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Repository.Infrastructure.Data
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {
        }

        // DbSet แต่ละตาราง
        public DbSet<Events> Events { get; set; } = null!;
        public DbSet<Packages> Packages { get; set; } = null!;
        public DbSet<EquipmentSets> EquipmentSets { get; set; } = null!;
        public DbSet<Permissions> Permissions { get; set; } = null!;
        public DbSet<Equipments> Equipments { get; set; } = null!;
        public DbSet<StaffPermissions> StaffPermissions { get; set; } = null!;
        public DbSet<EventExtraEquipments> EventExtraEquipments { get; set; } = null!;
        public DbSet<Staff> Staff { get; set; } = null!;
        public DbSet<Companies> Companies { get; set; } = null!;
        public DbSet<CompanyContacts> CompanyContacts { get; set; } = null!;
        public DbSet<Outsources> Outsources { get; set; } = null!;
        public DbSet<EventOutsources> EventOutsources { get; set; } = null!;
        public DbSet<EventStaff> EventStaff { get; set; } = null!;


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // จำเป็นต้องเขียนบรรทัดนี้ครับ
            modelBuilder.Entity<EquipmentSets>()
                .HasKey(es => new { es.EquipmentId, es.PackegeId });

            modelBuilder.Entity<EventExtraEquipments>()
                .HasKey(es => new { es.EquipmentId, es.EventId });

            modelBuilder.Entity<EventOutsources>()
                .HasKey(es => new { es.OutsourcesId, es.EventId });

            modelBuilder.Entity<StaffPermissions>()
                .HasKey(es => new { es.StaffId, es.PermissionId });

            modelBuilder.Entity<EventStaff>(entity =>
            {
                entity.HasKey(x => new { x.EventId, x.StaffId });

                entity.HasOne(x => x.Event)
                      .WithMany(e => e.EventStaff)     // ของคุณชื่อ EventStaff (singular) แต่เป็น ICollection
                      .HasForeignKey(x => x.EventId)
                      .OnDelete(DeleteBehavior.Cascade);   // ลบ Event -> ลบ rows ใน EventStaff ได้

                entity.HasOne(x => x.Staff)
                      .WithMany(s => s.EventStaffs)
                      .HasForeignKey(x => x.StaffId)
                      .OnDelete(DeleteBehavior.NoAction);  // สำคัญ: กัน cascade ซ้ำ
            });


            modelBuilder.Entity<Events>()
    .HasOne(e => e.CreatedByStaff)
    .WithMany(s => s.CreatedEvents)
    .HasForeignKey(e => e.CreatedByStaffId)
    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
