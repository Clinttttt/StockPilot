using MediatR;
using StockPilot.Application.Common.Model;
using StockPilot.Application.Dtos;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.Supplier.GetSupplierProducts
{
    public record GetSupplierProductsQuery(Guid SupplierId, int pageSize = 10, int PageNumber = 1) : IRequest<Result<PaginatedList<ProductDto>>>;

}
