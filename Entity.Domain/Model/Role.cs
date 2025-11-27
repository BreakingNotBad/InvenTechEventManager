namespace Entity.Domain.Model
{
    public class Role
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public string RoleName { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
