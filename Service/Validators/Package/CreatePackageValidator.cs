using Contracts.IRepository.BaseManager;
using FluentValidation;
using Service.Contracts.DTOs.Package;
using Service.Validators.EquipmentSets;
namespace Service.Validators.Package
{
    public class CreatePackageValidator : AbstractValidator<CreatePackageDto>
    {
        private readonly IRepositoryManager _repo;
        public CreatePackageValidator(IRepositoryManager repo)
        {
            _repo = repo;

            RuleFor(x => x.PackageName)
                .NotEmpty().WithMessage("Package name is required.")
                .MaximumLength(100).WithMessage("Package name must not exceed 100 characters.");

            RuleFor(x => x.EquipmentSets)
                .Must(list => list
                .GroupBy(x => x.EquipmentId)
                .All(g => g.Count() == 1))
                .WithMessage("Duplicate EquipmentId is not allowed.");

            RuleFor(x => x.EquipmentSets)
                .MustAsync(async (equipmentSets, cancellation) =>
                {
                    foreach (var item in equipmentSets)
                    {
                        if (!await _repo.Equipment.ExistsAsync(item.EquipmentId))
                            return false;
                    }
                    return true;
                })
                .WithMessage("One or more EquipmentId does not exist.");

            RuleForEach(x => x.EquipmentSets).SetValidator(new CreateEquipmentSetValidator());
        }
    }
}
