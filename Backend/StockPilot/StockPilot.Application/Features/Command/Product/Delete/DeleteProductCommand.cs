using MediatR;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Product.DeleteProduct
{
    public record DeleteProductCommand(Guid ProductId) : IRequest<Result<bool>>;
    
    
}
