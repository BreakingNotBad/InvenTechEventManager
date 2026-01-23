using FluentValidation;
using Service.Contracts.DTOs.Package;
using Service.Validators.EquipmentSets;
namespace Service.Validators.Package
{
    public class UpdatePackegeValidator : AbstractValidator<UpdatePackageDto>
    {
        public UpdatePackegeValidator()
        {
            RuleFor(x => x.PackageName)
                .NotEmpty().WithMessage("Package name is required.")
                .MaximumLength(100).WithMessage("Package name must not exceed 100 characters.");

            RuleForEach(x => x.EquipmentSets).SetValidator(new UpdateEquipmentSetValidator());
        }
    }
}
