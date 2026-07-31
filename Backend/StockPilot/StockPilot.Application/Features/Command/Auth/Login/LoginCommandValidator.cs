using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Auth.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        
        {
            RuleFor(s => s.UserName).NotEmpty().WithMessage("Username is empty");
            RuleFor(s => s.Password).NotEmpty().WithMessage("Password is not empty")
                .MaximumLength(20).WithMessage("Cannot exceed on 20 letter");
        }
    }
}
