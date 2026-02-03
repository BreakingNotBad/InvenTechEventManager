using FluentValidation;
using Presentation.Requests.Event;
using Service.Contracts.DTOs.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators.Event
{
    public class CreateEventOutsourceValidator :AbstractValidator<CreateEventOutsourceDto>
    {
        public CreateEventOutsourceValidator()
        {

        }
    }
}
