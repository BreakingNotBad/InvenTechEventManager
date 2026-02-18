using FluentValidation;
using Service.Contracts.DTOs.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators.Staff
{
    public class SetPasswordValidator : AbstractValidator<SetPasswordDto>
    {
        public SetPasswordValidator()
        {
            RuleFor(x => x.Token)
                .NotEmpty()
                .WithMessage("Token is required");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("NewPassword is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters")
                .Matches("[A-Z]").WithMessage("Password must contain uppercase letter")
                .Matches("[a-z]").WithMessage("Password must contain lowercase letter")
                .Matches("[0-9]").WithMessage("Password must contain number");
        }
    }
}
