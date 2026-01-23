using FluentValidation;
using Service.Contracts.DTOs.EquipmentSet;
namespace Service.Validators.EquipmentSets
{
    public class CreateEquipmentSetValidator : AbstractValidator<CreateEquipmentSetDto>
    {
        public CreateEquipmentSetValidator()
        {
            RuleFor(x => x.EquipmentId)
                .NotEmpty().WithMessage("EquipmentId is required.")
                .GreaterThan(0).WithMessage("EquipmentId must be greater than 0.");

            RuleFor(x => x.Quantity)
                .NotEmpty().WithMessage("Quantity is required.")
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");
        }
    }
}
