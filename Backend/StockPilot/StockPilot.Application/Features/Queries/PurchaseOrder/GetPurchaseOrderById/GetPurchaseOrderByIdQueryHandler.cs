using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Application.Dtos;
using StockPilot.Domain.Common;
using StockPilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.PurchaseOrder.GetPurchaseOrderById
{
    public class GetPurchaseOrderByIdQueryHandle(IAppDbContext context) : IRequestHandler<GetPurchaseOrderByIdQuery, Result<PurchaseOrderDto>>
    {
        public async Task<Result<PurchaseOrderDto>> Handle(GetPurchaseOrderByIdQuery request, CancellationToken cancellationToken)
        {

            var purchaseOrder = await context.purchaseOrders
                .AsNoTracking()
                .Where(s=> s.Id == request.PurchaseOrderId)
                .Select(s => new PurchaseOrderDto(
                    PurchaseOrderId: s.Id,
                    PoNumber: s.PoNumber,
                    SupplierName: s.Supplier.FullName,
                    OrderDate: s.OrderDate,
                    ExpectedDeliveryDate: s.ExpectedDeliveryDate,
                    Status: s.orderStatus,
                    ItemCount: s.Items.Count,
                    TotalAmount: s.Items.Sum(s => s.QuantityOrdered * s.UnitCost),
                    Remarks: s.Remarks ?? string.Empty,
                    Items: s.Items.Select(item => new PurchaseOrderItemDto(
                        ProductId: item.ProductId,
                        ProductName: item.Product.Name ?? string.Empty,
                        QuantityOrdered: item.QuantityOrdered,
                        QuantityReceived: item.QuantityReceived,
                        UnitCost: item.UnitCost
                        )).ToList()
                    )).FirstOrDefaultAsync(cancellationToken);

            if (purchaseOrder is null)
                return Result<PurchaseOrderDto>.NotFound("Purchase order not found");

            return Result<PurchaseOrderDto>.Success(purchaseOrder);
        }
    }
}
