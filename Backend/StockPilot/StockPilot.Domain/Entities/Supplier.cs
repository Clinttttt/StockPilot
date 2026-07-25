using StockPilot.Domain.Common;
using StockPilot.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Domain.Entities
{
    public class Supplier : Contact
    {

        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();

        public Supplier() { }

        public static Supplier Create(string fullName,
           string phoneNumber,
           string email,
           string address)
        {
            return new Supplier
            {
                Id = new Guid(),
                IsActive = true,
                FullName = fullName,
                PhoneNumber = phoneNumber,
                Email = email,
                Address = address,
            };
        }
        public void Update(string name, string phoneNumber, string email, string address)
        {
            FullName = name ?? FullName;
            PhoneNumber = phoneNumber ?? PhoneNumber;
            Email = email ?? Email;
        }
        public void SetStatus(bool IsActive)
        {   
                this.IsActive = true;              
        }
    }
}
