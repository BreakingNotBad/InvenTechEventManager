using FluentValidation;
using Presentation.Extensions;
using Presentation.Requests.Staff;

namespace Presentation.Validators
{
    public class UpdateStaffRequestValidator : AbstractValidator<UpdateStaffRequest>
    {
        public UpdateStaffRequestValidator()
        {
            // เช็คไฟล์ (ถ้ามีการแนบไฟล์ใหม่มา)
            RuleFor(x => x.AvatarFile)
                .Must(file => file == null || file.Length <= 5 * 1024 * 1024)
                .WithMessage("Avatar file size must not exceed 5 MB.")
                .Must(file => file.IsValidImageExtension())
                .WithMessage("Only .jpg, .jpeg, .png, and .gif files are allowed.");
        }
    }
}
