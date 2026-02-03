using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Service.Contracts.DTOs.Event;

namespace Service.Validators.Event
{
    public class UpdateEventOutsourceValidator : AbstractValidator<UpdateEventOutsourceDto>
    {
        public UpdateEventOutsourceValidator() { }
    }
}
