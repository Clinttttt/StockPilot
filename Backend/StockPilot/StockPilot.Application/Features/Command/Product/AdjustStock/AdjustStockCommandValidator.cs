using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace StockPilot.Application.Features.Command.Product.AdjustStock
{
    public class AdjustStockCommandValidator : AbstractValidator<AdjustStockCommand>
    {
        public AdjustStockCommandValidator()
        {
            RuleFor(s=> s.Remarks).MaximumLength(100);
        }
    }
}
