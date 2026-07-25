using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;

namespace StockPilot.Domain.Entities
{
    public class PurchaseOrder : AuditableEntity
    {
        public string PoNumber { get; set; } = string.Empty;

        public Guid? SupplierId { get; set; }
        public Supplier Supplier { get; set; } = null!;
        public DateTime? OrderDate { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string? Remarks { get; set; }
        public PurchaseOrderStatus? orderStatus { get; set; }

        public ICollection<PurchaseOrderItem> Items { get; set; } = new List<PurchaseOrderItem>();



        public static PurchaseOrder Create(Guid SupplierId, DateTime? ExpectedDeliveryDate, string Remarks)
        {
            return new PurchaseOrder    
            {
                Id = Guid.NewGuid(),
                SupplierId = SupplierId,
                OrderDate = DateTime.Now,
                orderStatus = PurchaseOrderStatus.Draft,
                ExpectedDeliveryDate = ExpectedDeliveryDate,
                Remarks = Remarks,
            };

        }

        public readonly List<PurchaseOrderItem> _items = [];
        public IReadOnlyCollection<PurchaseOrderItem> UpdatedPurchase => _items;

        public void Update(IEnumerable<PurchaseOrderItem> items)
        {
            _items.Clear();
            _items.AddRange(items);
        }

        public static string GeneratePoNum(Guid Id) => $"PO-{DateTime.Now:yyyy:MMdd}-{Id}";


    }
    public enum PurchaseOrderStatus
    {
        Draft,
        Sent,
        PartiallyReceived,
        Received,
        Cancelled
    }


}
