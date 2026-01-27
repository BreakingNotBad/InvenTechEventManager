using FluentValidation;
using Service.Contracts.DTOs.Outsource;
namespace Service.Validators.Outsource
{
    public class UpdateOutsourceValidator : AbstractValidator<UpdateOutsourceDto>
    {
        public UpdateOutsourceValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("outsourceFullName is required.")
                .MaximumLength(255).WithMessage("FullName must not exceed 255 characters.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(255).WithMessage("Email must not exceed 255 characters.");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^0\d{9}$")
                .WithMessage("Invalid phone number format.")
                .MaximumLength(50).WithMessage("Phone number must not exceed 50 characters.");
        }
    }
}
