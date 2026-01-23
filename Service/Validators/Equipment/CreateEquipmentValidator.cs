
using FluentValidation;
using Service.Contracts.DTOs.Equipment;
namespace Service.Validators.Equipment
{
    public class CreateEquipmentValidator : AbstractValidator<CreateEquipmentDto>
    {
        public CreateEquipmentValidator()
        {
            RuleFor(e => e.EquipmentName)
                .NotEmpty().WithMessage("Equipment name is required.")
                .MaximumLength(100).WithMessage("Equipment name must not exceed 100 characters.");

            RuleFor(e => e.CategoryId)
                .GreaterThan(0).WithMessage("Category ID must be a positive integer.");
        }
    }
}
