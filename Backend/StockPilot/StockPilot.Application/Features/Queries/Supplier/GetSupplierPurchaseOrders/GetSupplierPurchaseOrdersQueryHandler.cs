using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.Supplier.GetSupplierPurchaseOrders
{
    public class GetSupplierPurchaseOrdersQueryHandler(IAppDbContext context) : IRequestHandler<GetSupplierPurchaseOrdersQuery, Result<List<SupplierPurchaseOrderDto>>>
    {
        public async Task<Result<List<SupplierPurchaseOrderDto>>> Handle(GetSupplierPurchaseOrdersQuery request, CancellationToken cancellationToken)
        {

            var purchaserders = context.suppliers.AsNoTracking()
                .Where(s => s.Id == request.SupplierId);

            var dtos = await purchaserders.Select(s => new SupplierPurchaseOrderDto
            {
                product = s.Products.Select(s=> new PurchaseProductDto
                {
                    ProductId = s.Id,
                    ProductName = s.Name
                }).ToList(),
                purchaseDto = s.PurchaseOrders
                .Where(s=> request.OrderStatus.HasValue && s.orderStatus == request.OrderStatus)
                .Where(s=> s.OrderDate.HasValue && DateOnly.FromDateTime(s.OrderDate.Value) >= request.FromDate)
                .Where(s => s.OrderDate.HasValue && DateOnly.FromDateTime(s.OrderDate.Value) >= request.ToDate)
                .Select(s=> new PurchaseOrderDto
                {
                    PoNumber = s.PoNumber,
                    SupplierId = s.SupplierId,
                    OrderDate =  s.OrderDate,
                    ExpectedDeliveryDate = s.ExpectedDeliveryDate,
                    ReceivedDate = s.ReceivedDate,
                    Remarks = s.Remarks,
                    orderStatus = s.orderStatus
                }).ToList()          
            }).ToListAsync();

            return Result<List<SupplierPurchaseOrderDto>>.Success(dtos);
        }
    }
}
