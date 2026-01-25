using FluentValidation;
using Presentation.Extensions;
using Presentation.Requests.Staff;

namespace Presentation.Validators
{
    public class CreateStaffRequestValidator : AbstractValidator<CreateStaffRequest>
    {
        public CreateStaffRequestValidator()
        {
            // กฎสำหรับไฟล์ Avatar (ถ้ามีไฟล์แนบมา)
            RuleFor(x => x.AvatarFile)
                .Must(file => file == null || file.Length <= 5 * 1024 * 1024)
                .WithMessage("Avatar file size must not exceed 5 MB.")
                .Must(file => file.IsValidImageExtension())
                .WithMessage("Only .jpg, .jpeg, .png, and .gif files are allowed.");
        }
    }
}
