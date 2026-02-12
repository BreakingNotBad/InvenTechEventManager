using Shared.RequestFeatures.Enums;

namespace Shared.RequestFeatures.Parameters
{
    public class StaffParameter
    {
        public string? FullName { get; set; }
        public string? Role { get; set; }
        public bool? IsDeleted { get; set; }
        public DateOnly? Date { get; set; }
        public TimePeriod? Period { get; set; }
    }
}
