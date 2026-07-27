using MediatR;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Product.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string ProductName, string desciption,
        string SKU, string Unit, decimal? costPrice,
        int? sellingPrice, int? currentStock, int minimumStock) : IRequest<Result>;
   
}
