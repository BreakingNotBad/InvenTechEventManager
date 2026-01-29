using System.ComponentModel;

namespace Service.Contracts.DTOs.Outsource
{
    public class UpdateOutsourceDto
    {
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        [DefaultValue(false)]
        public bool? IsDeleted { get; set; }
    }
}
