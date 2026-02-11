using Shared.RequestFeatures.Enums;

namespace Shared.RequestFeatures.Parameters
{
    public class CheckStaffStatusParameter
    {
        public DateOnly? Date { get; set; }
        public TimePeriod? Period { get; set; }
    }
}
