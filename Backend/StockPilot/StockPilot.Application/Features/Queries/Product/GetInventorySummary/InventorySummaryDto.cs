using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.Product.GetInventorySummary
{
    public sealed record InventorySummaryDto(
     int TotalProducts,
     int ActiveProducts,
     int? TotalUnitsInStock,
     int? LowStockProducts,
     int? OutOfStockProducts,
     decimal? TotalInventoryCost,
     decimal? PotentialSalesValue
 );
}
