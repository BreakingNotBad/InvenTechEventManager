using FluentValidation;
using Service.Contracts.DTOs.Company;
using Service.Validators.CompanyContact;

namespace Service.Validators.Company
{
    public class UpdateCompanyValidator : AbstractValidator<UpdateCompanyDto>
    {
        public UpdateCompanyValidator()
        {
            RuleFor(x => x.CompanyName)
                .NotEmpty().WithMessage("Company name is required")
                .MaximumLength(255).WithMessage("Company name must not exceed 255 characters");

            RuleFor(x => x.Address)
                .MaximumLength(500).WithMessage("Address must not exceed 500 characters");

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90m, 90m).WithMessage("Latitude must be between -90 and 90")
                .When(x => x.Latitude.HasValue);

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180m, 180).WithMessage("Longitude must be between -180 and 180")
                .When(x => x.Longitude.HasValue);

            RuleFor(x => x.CompanyContacts)
                .Must(CompanyContact => CompanyContact.Count(c => c.IsPrimary) <= 1).WithMessage("Only one primary contact is allowed.");

            RuleForEach(x => x.CompanyContacts)
                .SetValidator(new UpdateCompanyContactValidator());
        }
    }
}
