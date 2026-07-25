using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Domain.Entities
{
    public class Category : AuditableEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Product>? Products { get; set; }

        public static Category Create(string name, string description)
        {
            return new Category
            {
                Id = Guid.NewGuid(),
                Name = name,
                Description = description,
                IsActive = true
            };
        }
    }
}
