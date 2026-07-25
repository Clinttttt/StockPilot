using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Application.Features.Queries.PurchaseOrder.GetPurchaseOrderById;
using StockPilot.Application.Features.Queries.Supplier.GetSupplierPurchaseOrders;
using StockPilot.Domain.Common;
using StockPilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using PurchaseOrderDto = StockPilot.Application.Features.Queries.PurchaseOrder.GetPurchaseOrderById.PurchaseOrderDto;

namespace StockPilot.Application.Features.Queries.PurchaseOrder.GetPurchaseOrderForReceiving
{
    public class GetPurchaseOrderForReceivingQueryHandler(IAppDbContext context) : IRequestHandler<GetPurchaseOrderForReceivingQuery, Result<PurchaseOrderForReceivingDto>>
    {
        public  async Task<Result<PurchaseOrderForReceivingDto>> Handle(GetPurchaseOrderForReceivingQuery request, CancellationToken cancellationToken)
        {
            var purchaseOrder = await context.purchaseOrders
                .AsNoTracking()
                .Where(s => s.Id == request.PurchaseOrderId && (s.orderStatus == PurchaseOrderStatus.Received || s.orderStatus == PurchaseOrderStatus.PartiallyReceived))
                .Select(s => new PurchaseOrderForReceivingDto(
                    PurchaseOrderId: s.Id,
                    PoNumber: s.PoNumber,
                    SupplierName: s.Supplier.FullName,
                    OrderDate: s.OrderDate,
                    ExpectedDeliveryDate: s.ExpectedDeliveryDate,
                    Status: s.orderStatus,
                    ItemCount: s.Items.Count,
                    TotalAmount: s.Items.Sum(s => s.QuantityOrdered * s.UnitCost),
                    Remarks: s.Remarks ?? string.Empty,
                    Items: s.Items
                    .Where(item=> item.QuantityReceived < item.QuantityOrdered)
                    .Select(order => new PurchaseOrderItemForReceivingDto(
                        PurchaseOrderItemId: order.Id,
                        ProductId: order.ProductId,
                        ProductName: order.Product.Name ?? string.Empty,
                        QuantityOrdered: order.QuantityOrdered,
                        QuantityReceived: order.QuantityReceived,
                        RemainingQuantity: order.QuantityOrdered - order.QuantityReceived
                        )).ToList()
                    )).FirstOrDefaultAsync(cancellationToken);

            if (purchaseOrder is null)
                return Result<PurchaseOrderForReceivingDto>.NotFound("Purchase order not found");

            return Result<PurchaseOrderForReceivingDto>.Success(purchaseOrder);
        }
    }
}
