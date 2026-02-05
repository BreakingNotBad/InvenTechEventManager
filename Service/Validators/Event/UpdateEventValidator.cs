using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Contracts.IRepository.BaseManager;
using Service.Contracts.DTOs.Event;
namespace Service.Validators.Event
{
    public class UpdateEventValidator : AbstractValidator<UpdateEventDto>
    {
        private readonly IRepositoryManager _repo;
        public UpdateEventValidator(IRepositoryManager repo)
        {
            _repo = repo;
            RuleFor(x => x.EventName)
                .NotEmpty()
                .WithMessage("Event name is required")
                .MaximumLength(255)
                .WithMessage("Event name must not exceed 255 characters");

            RuleFor(x => x.CompanyId)
                .GreaterThan(0).WithMessage("Company is required");


            RuleFor(x => x.MeetingDate)
                .NotNull()
                .WithMessage("Meeting date is required")
                .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Meeting date must not be in the past");

            RuleFor(x => x.RegistrationTime)
                .NotEmpty()
                .WithMessage("Registration time is required");

            RuleFor(x => x.StartTime)
                .NotEmpty()
                .WithMessage("Start time is required");

            RuleFor(x => x.EndTime)
                .NotEmpty()
                .WithMessage("End time is required")
                .Must((x, endTime) => x.StartTime != default)
                .WithMessage("Please select start time first")
                .Must((x, endTime) => endTime > x.StartTime)
                .WithMessage("End time must be after start time");


            RuleFor(x => x.EventStaffs)
                .NotNull()
                .WithMessage("Event staff is required")
                .NotEmpty()
                .WithMessage("At least one staff is required in event");

            RuleFor(x => x.EventStaffs)
                .Must(list =>
                    list.Select(x => new { x.StaffId, x.RoleId })
                        .Distinct().Count() == list.Count)
                .WithMessage("Duplicate staff role is not allowed");

            RuleFor(x => x.EventStaffs)
                .MustAsync(async (list, ct) =>
                    await _repo.Staff.AllStaffIdsExistAsync(
                        list.Select(x => x.StaffId)))
                .WithMessage("One or more staff do not exist");

            RuleFor(x => x.EventStaffs)
                .MustAsync(async (list, ct) =>
                    await _repo.Role.AllRoleIdsExistAsync(
                        list.Select(x => x.RoleId)))
                .WithMessage("One or more role do not exist");

            RuleForEach(x => x.EventExtraEquipments).SetValidator(new UpdateEventExtraEquipmentValidator());
            RuleFor(x => x.EventExtraEquipments)
                .Must(list =>
                    list.Select(e => e.EquipmentId).Distinct().Count() == list.Count)
                .WithMessage("Duplicate equipment is not allowed");

            RuleFor(x => x.EventExtraEquipments)
                .MustAsync(async (list, ct) =>
                    await _repo.Equipment.AllEquipmentIdsExistAsync(
                        list.Select(e => e.EquipmentId)))
                .WithMessage("One or more equipment do not exist");


                RuleForEach(x => x.EventOutsources)
                    .SetValidator(new UpdateEventOutsourceValidator());

                RuleFor(x => x.EventOutsources)
                    .Must(list =>
                        list.Select(o => o.OutsourceId).Distinct().Count() == list.Count)
                    .WithMessage("Duplicate outsource is not allowed");

                RuleFor(x => x.EventOutsources)
                    .MustAsync(async (list, ct) =>
                        await _repo.Outsource.AllOutsourceIdsExistAsync(
                            list.Select(o => o.OutsourceId)))
                    .WithMessage("One or more outsource do not exist");

            RuleFor(x => x.EventOutsources)
                .MustAsync(async (list, ct) =>
                    await _repo.Role.AllRoleIdsExistAsync(
                        list.Select(x => x.RoleId)))
                .WithMessage("One or more role do not exist");

        }
    }
}
