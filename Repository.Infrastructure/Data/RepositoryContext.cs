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
        public DbSet<QuoteStatus> QuoteStatuses { get; set; } = null!;
        public DbSet<Package> Packages { get; set; } = null!;
        public DbSet<EquipmentSet> EquipmentSets { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Permission> Permissions { get; set; } = null!;
        public DbSet<Role> Roles { get; set; } = null!;
        public DbSet<Equipment> Equipment { get; set; } = null!;
        public DbSet<UserPermission> UserPermissions { get; set; } = null!;
        public DbSet<EventEquipment> EventEquipment { get; set; } = null!;
        public DbSet<EquipmentSetContent> EquipmentSetContents { get; set; } = null!;
        public DbSet<EventStaff> EventStaff { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<CompanyContact> CompanyContacts { get; set; } = null!;
        public DbSet<GuestUser> GuestUsers { get; set; } = null!;
        public DbSet<EventGuest> EventGuests { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // map ชื่อ table ให้ตรงกับใน SQL
            modelBuilder.Entity<Event>().ToTable("Events").HasKey(e => e.EventId);
            modelBuilder.Entity<QuoteStatus>().ToTable("QuoteStatuses").HasKey(q => q.QuoteStatusId);
            modelBuilder.Entity<Package>().ToTable("Packages").HasKey(p => p.PackageId);
            modelBuilder.Entity<EquipmentSet>().ToTable("EquipmentSets").HasKey(e => e.EquipmentSetId);
            modelBuilder.Entity<User>().ToTable("Users").HasKey(u => u.UserId);
            modelBuilder.Entity<Permission>().ToTable("Permissions").HasKey(p => p.PermissionId);
            modelBuilder.Entity<Role>().ToTable("Roles").HasKey(r => r.RoleId);
            modelBuilder.Entity<Equipment>().ToTable("Equipment").HasKey(e => e.EquipmentId);
            modelBuilder.Entity<UserPermission>().ToTable("UserPermissions").HasKey(up => new { up.UserId, up.PermissionId });
            modelBuilder.Entity<EventEquipment>().ToTable("EventEquipment").HasKey(ee => new { ee.EventId, ee.EquipmentId });
            modelBuilder.Entity<EquipmentSetContent>().ToTable("EquipmentSetContents").HasKey(ec => new { ec.EquipmentSetId, ec.EquipmentId });
            modelBuilder.Entity<EventStaff>().ToTable("EventStaff").HasKey(ea => ea.EventStaffId);
            modelBuilder.Entity<Company>().ToTable("Companies").HasKey(c => c.CompanyId);
            modelBuilder.Entity<CompanyContact>().ToTable("CompanyContacts").HasKey(c => c.CompanyContactId);
            modelBuilder.Entity<GuestUser>().ToTable("GuestUsers").HasKey(g => g.GuestUserId);
            modelBuilder.Entity<EventGuest>().ToTable("EventGuests").HasKey(eg => new { eg.EventId, eg.GuestUserId });

            base.OnModelCreating(modelBuilder);
        }
    }
}