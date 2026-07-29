using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Product.DeleteProduct
{
    public class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductValidator() 
        {
            RuleFor(s => s.ProductId).NotEmpty().WithMessage("Product id cannot be empty");
           
        }
    }
}
