using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Domain.Entities
{
    public class PurchaseOrderItem : BaseEntity
    {
        public Guid PurchaseOrderId { get; set; }
        public PurchaseOrder PurchaseOrder { get; set; } = null!;

        public Guid ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public int QuantityOrdered { get; set; }
        public int QuantityReceived { get; set; }

        public decimal UnitCost { get; set; }

        public decimal LineTotal => QuantityOrdered * UnitCost;

        public static PurchaseOrderItem Update(Guid ProductId, int QuantityOrdered, decimal UnitCost, Guid PurchaseOrderId)
        {
            return new PurchaseOrderItem
            {
                ProductId = ProductId,
                QuantityOrdered = QuantityOrdered,
                UnitCost = UnitCost,
                PurchaseOrderId = PurchaseOrderId,
            };
        }

    
    }
}
