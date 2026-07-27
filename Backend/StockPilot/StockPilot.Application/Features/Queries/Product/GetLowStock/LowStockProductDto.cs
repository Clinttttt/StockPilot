using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.Product.GetLowStock
{
    public sealed record LowStockProductDto(
     Guid ProductId,
     string ProductName,
     string Sku,
     string? CategoryName,
     string Unit,
     int? CurrentStock,
     int? MinimumStock,
     int ReorderQuantity,
     int? ShortageQuantity,
     LowStockLevel Level
 );
    public enum LowStockLevel
    {
        OutOfStock = 1,
        Critical = 2,
        LowStock = 3
    }
}
