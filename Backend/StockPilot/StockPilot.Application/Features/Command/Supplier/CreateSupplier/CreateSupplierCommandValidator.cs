using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Supplier.CreateSupplier
{
    public  class CreateSupplierCommandValidator : AbstractValidator<CreateSupplierCommand>
    {
        public CreateSupplierCommandValidator() 
        {
            RuleFor(s => s.FullName).NotEmpty().WithMessage(" Full name is required")
                .MaximumLength(100).WithMessage("Exceeded maximum length");

            RuleFor(s => s.Email).EmailAddress().WithMessage("Invalid email address");
            RuleFor(s => s.Address).MaximumLength(200).WithMessage("Exceeded maximum length");                
        }
    }
}
