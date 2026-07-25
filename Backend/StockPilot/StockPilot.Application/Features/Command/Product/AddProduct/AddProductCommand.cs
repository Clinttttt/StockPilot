using MediatR;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Product.AddProduct.cs
{
    public record AddProductCommand(string productName, string sku, int CurrentStock, int MinimumStock, string ImageUrl, decimal CostPrice, string Unit, Guid CategoryId) : IRequest<Result>;
   
 



}
