using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Category
{
    public class AddCategoryValidator : AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryValidator() 
        {
            RuleFor(s => s.name).NotEmpty().WithMessage("name is required")
                .MaximumLength(50).WithMessage("name exceeds limits");
            RuleFor(s=> s.description).MaximumLength(100).WithMessage("description exceeds limits");
        }
    }
}
