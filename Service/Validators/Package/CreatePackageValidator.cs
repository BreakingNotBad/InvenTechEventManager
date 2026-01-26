using FluentValidation;
using Service.Contracts.DTOs.Package;
using Service.Validators.EquipmentSets;
namespace Service.Validators.Package
{
    public class CreatePackageValidator : AbstractValidator<CreatePackageDto>
    {
        public CreatePackageValidator()
        {
            RuleFor(x => x.PackageName)
                .NotEmpty().WithMessage("Package name is required.")
                .MaximumLength(100).WithMessage("Package name must not exceed 100 characters.");

            RuleFor(x => x.EquipmentSets)
                .Must(list => list
                .GroupBy(x => x.EquipmentId)
                .All(g => g.Count() == 1))
                .WithMessage("Duplicate EquipmentId is not allowed.");

            RuleForEach(x => x.EquipmentSets).SetValidator(new CreateEquipmentSetValidator());
        }
    }
}
