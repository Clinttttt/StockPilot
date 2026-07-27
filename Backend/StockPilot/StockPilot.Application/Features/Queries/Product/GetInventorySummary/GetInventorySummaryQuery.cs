using MediatR;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.Product.GetInventorySummary
{
    public class GetInventorySummaryQuery() : IRequest<Result<InventorySummaryDto>>;


}
