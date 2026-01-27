using FluentValidation;
using Service.Contracts.DTOs.Staff;

namespace Service.Validators.Staff
{
    public class UpdateStaffValidator : AbstractValidator<UpdateStaffDto>
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
                .Matches(@"^0\d{9}$")
                .WithMessage("Invalid phone number format.")
                .MaximumLength(50).WithMessage("Phone number must not exceed 50 characters.");

            RuleFor(x => x.RoleIds)
                .NotEmpty()
                .WithMessage("At least one RoleId must be provided")
                .NotNull()
                .WithMessage("RoleIds cannot be null");
        }
    }
}
