using FluentValidation;
using Presentation.Requests.Staff;

namespace Presentation.Validators.Staff
{
    public class UpdateStaffValidator : AbstractValidator<UpdateStaffRequest>
    {
        public UpdateStaffValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("staffFull name is required")
                .MaximumLength(255)
                .WithMessage("Full name must not exceed 255 characters");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Invalid email format")
                .MaximumLength(255)
                .WithMessage("Email must not exceed 255 characters");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Invalid phone number format")
                .MaximumLength(50)
                .WithMessage("Phone number must not exceed 50 characters");

            RuleFor(x => x.RoleIds)
                .NotEmpty()
                .WithMessage("At least one RoleId must be provided")
                .NotNull()
                .WithMessage("RoleIds cannot be null");

            RuleFor(RuleFor => RuleFor.AvatarFile)
                .Must(file => file == null || file.Length <= 5 * 1024 * 1024) // 5 MB limit
                .WithMessage("Avatar file size must not exceed 5 MB");
        }
    }
}
