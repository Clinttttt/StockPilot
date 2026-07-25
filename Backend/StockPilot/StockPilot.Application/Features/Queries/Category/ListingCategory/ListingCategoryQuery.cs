using MediatR;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.Category.ListingCategory
{
    public record ListingCategoryQuery : IRequest<Result<List<ListingCategoryQueryDto>>>;
    
}
