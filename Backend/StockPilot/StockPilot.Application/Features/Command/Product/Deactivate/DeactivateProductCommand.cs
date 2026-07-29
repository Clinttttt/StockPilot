using MediatR;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.Product.DeactivateProduct
{
    public record DeactivateProductCommand(Guid ProductId) : IRequest<Result>;
   
}
