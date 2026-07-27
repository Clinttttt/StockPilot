using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Product.AddProduct.cs
{
    public class AddProductCommandValidator : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidator()
        {
            RuleFor(s => s.productName).NotEmpty().WithMessage("Product name cannot be empty")
                .MaximumLength(100).WithMessage("Cannot Exceed 100 letter");
            RuleFor(s => s.sku).NotEmpty().WithMessage("Sku cannot be empty")
                .MaximumLength(100).WithMessage("Cannot Exceed 100 letter");
            RuleFor(s => s.CurrentStock).GreaterThan(0).WithMessage("Current stock muste be greater than 0");
            RuleFor(s=> s.CostPrice).GreaterThan(0).WithMessage("Cost price muste be greater than 0");
            RuleFor(s=> s.MinimumStock).GreaterThan(0).WithMessage("Minimum stock muste be greater than 0");
            RuleFor(s=> s.CostPrice).GreaterThan(0).WithMessage("Cost price muste be greater than 0");
        }
    }
}
