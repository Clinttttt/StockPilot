using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.Supplier.GetSupplierById
{
    public class GetSupplierByIdQueryHandler(IAppDbContext context) : IRequestHandler<GetSupplierByIdQuery, Result<GetSupplierDto>>
    {
        public  async Task<Result<GetSupplierDto>> Handle(GetSupplierByIdQuery request, CancellationToken cancellationToken)
        {
            var supplier = await context.suppliers
                .AsNoTracking()
                .Where(s=> s.Id == request.SupplierId)
                .Select(s => new GetSupplierDto
                {
                    SupplierId = s.Id,
                    FullName = s.FullName,
                    PhoneNumber = s.PhoneNumber,
                    LastOrder = s.PurchaseOrders.OrderByDescending
                    (s => s.OrderDate).Select(s => s.OrderDate).FirstOrDefault(),
                    IsActive = s.IsActive,
                    Email = s.Email,
                    Address = s.Address,
                    SuppliedProductCount = s.PurchaseOrders
                    .SelectMany(s => s.Items)
                    .Select(s => s.ProductId)
                    .Distinct()
                    .Count(),
                    PurchaseOrderCount = s.PurchaseOrders.Count()
                }).FirstOrDefaultAsync(cancellationToken);
            

            if (supplier is null) return Result<GetSupplierDto>.NotFound("Supplier not found");
            return Result<GetSupplierDto>.Success(supplier);
        } 
    }
}
