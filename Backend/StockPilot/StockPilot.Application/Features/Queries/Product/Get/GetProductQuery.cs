using MediatR;
using StockPilot.Application.Dtos;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.Product.GetProduct
{
    public record GetProductQuery(Guid ProductId) : IRequest<Result<ProductDto>>;
   
}
