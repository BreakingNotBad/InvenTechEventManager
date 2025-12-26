namespace Entity.Domain.Model
{
    public class Permissions
    {
        public int PermissionId { get; set; }
        public string PermissionName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ICollection<StaffPermissions> StaffPermissions { get; set;; } = new List<StaffPermissions>();
    }
}
