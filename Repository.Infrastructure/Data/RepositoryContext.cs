using Entity.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Repository.Infrastructure.Data
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {
        }

        // DbSet แต่ละตาราง
        public DbSet<Event> Events { get; set; } = null!;
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

        }
    }
}
