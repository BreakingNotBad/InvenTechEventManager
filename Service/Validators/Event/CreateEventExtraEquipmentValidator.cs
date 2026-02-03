using FluentValidation;
using Service.Contracts.DTOs.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators.Event
{
    public class CreateEventExtraEquipmentValidator : AbstractValidator<CreateEventExtraEquipmentDto>
    {
        public CreateEventExtraEquipmentValidator()
        {

        }
    }
}
