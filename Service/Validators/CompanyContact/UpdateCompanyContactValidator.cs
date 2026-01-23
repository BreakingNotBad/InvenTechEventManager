using FluentValidation;
using Service.Contracts.DTOs.CompanyContact;
namespace Service.Validators.CompanyContact
{
    public class UpdateCompanyContactValidator : AbstractValidator<UpdateCompanyContactDto>
    {
        public UpdateCompanyContactValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required")
                .MaximumLength(255).WithMessage("Full name must not exceed 255 characters");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(255).WithMessage("Email must not exceed 255 characters");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Invalid phone number format")
                .MaximumLength(50).WithMessage("Phone number must not exceed 50 characters");

            RuleFor(x => x.Position)
                .MaximumLength(100).WithMessage("Position must not exceed 100 characters");
        }
    }
}
