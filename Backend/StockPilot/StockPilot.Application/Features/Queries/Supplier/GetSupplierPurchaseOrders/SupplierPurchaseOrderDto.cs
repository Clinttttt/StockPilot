using StockPilot.Application.Dtos;
using StockPilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Queries.Supplier.GetSupplierPurchaseOrders
{
    public class SupplierPurchaseOrderDto
    {
       public List<PurchaseProductDto>? product {  get; set; } 
       public List<PurchaseOrderDto>? purchaseDto { get; set; }

    }
    public class PurchaseProductDto
    { 
        public Guid ProductId { get; set; }
        public string? ProductName { get; set; }
    }

    public class PurchaseOrderDto
    {
        public string? PoNumber { get; set; }
        public Guid? SupplierId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string? Remarks { get; set; }
        public PurchaseOrderStatus? orderStatus { get; set; }
    }


}
