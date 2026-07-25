using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Extensions;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Application.Common.Model;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.Supplier.GetSupplier
{
    public class GetSupplierQueryHandler(IAppDbContext context) : IRequestHandler<GetSupplierQuery, Result<PaginatedList<SupplierListItemDto>>>
    {
        public async Task<Result<PaginatedList<SupplierListItemDto>>> Handle(GetSupplierQuery request, CancellationToken cancellationToken)
        {
            var suppliers = context.suppliers.AsNoTracking();


            if (string.IsNullOrWhiteSpace(request.search))
            {
                suppliers = suppliers.Where(s => request.search.Contains(s.FullName));
            }

            var filtered = suppliers.OrderByDescending(s => s.FullName)
                .Select(s => new SupplierListItemDto 
                { 
                    SupplierId = s.Id,
                    Name = s.FullName,
                    ContactPerson = s.PhoneNumber,
                    IsActive = s.IsActive,
                    SuppliedProductCount = s.Products.Count(),
                    PurchaseOrderCount = s.PurchaseOrders.Count(),
                });

            var paginated = await QueryableExtensions.PaginatedAsync(filtered, request.pageNumber, request.pageSize);

            return Result<PaginatedList<SupplierListItemDto>>.Success(paginated);
        }
    }
}
