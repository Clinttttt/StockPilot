using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace StockPilot.Domain.Entities
{
    public class StockMovement : AuditableEntity
    {

        public Product Product { get; set; } = null!;
        public Guid ProductId { get; set; }
        public StockMovementType Type { get; set; }

        public int Quantity { get; set; }

        public string? ReferenceNo { get; set; }
        public string? Reason { get; set; }
        public string? Remarks { get; set; }
        public Guid? PurchaseOrderId { get; set; }
        public PurchaseOrder? PurchaseOrder { get; set; }

        public DateTime MovementDate { get; set; }
        public StockMovement() { }

        public static StockMovement Create(Guid ProductId,
            StockMovementType Type,
            int Quantity,
            string? Reason,
            string? Remarks,
            Guid? PurchaseOrderId = null
            )
        {
            return new StockMovement
            {
                ProductId = ProductId,
                Type = Type,
                Quantity = Quantity,
                Reason = Reason,
                Remarks = Remarks,
                PurchaseOrderId = PurchaseOrderId != Guid.Empty ? PurchaseOrderId : Guid.Empty,
                ReferenceNo = GenerateRefNo()
            };
        }


        public  static string GenerateRefNo() => Random.Shared.Next(1_000_000, 10_000_000).ToString();
        public enum StockMovementType
        {
            StockIn = 1,
            StockOut = 2,
            Adjustment = 3,
            Return = 4
        }
    }
}