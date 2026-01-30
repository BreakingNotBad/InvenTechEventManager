using Service.Contracts.DTOs.Outsource;
using Service.Contracts.DTOs.Role;

public class EventOutsourceDto
{
    public OutsourceDto Outsource { get; set; } = null!;
    public RoleDto Role { get; set; } = null!;
}
