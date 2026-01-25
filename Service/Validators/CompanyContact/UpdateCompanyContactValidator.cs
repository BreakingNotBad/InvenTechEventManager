using FluentValidation;
using Service.Contracts.DTOs.CompanyContact;

namespace Service.Validators.CompanyContact
{
    public class UpdateCompanyContactValidator : AbstractValidator<UpdateCompanyContactDto>
    {
        public UpdateCompanyContactValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Full name is required")
                .MaximumLength(255)
                .WithMessage("Full name must not exceed 255 characters");

            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Invalid email format")
                .MaximumLength(255)
                .WithMessage("Email must not exceed 255 characters");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^0\d{9}$")
                .WithMessage("Phone number must be a valid Thai mobile number");

            RuleFor(x => x.Position)
                .MaximumLength(100)
                .WithMessage("Position must not exceed 100 characters");
        }
    }
}
