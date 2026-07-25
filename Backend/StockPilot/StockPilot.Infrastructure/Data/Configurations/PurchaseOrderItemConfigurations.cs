using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StockPilot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Infrastructure.Data.Configurations
{
    public class PurchaseOrderItemConfigurations() : IEntityTypeConfiguration<PurchaseOrderItem>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderItem> builder)
        {
            builder.Property(s => s.QuantityOrdered)
                 .IsRequired();

            builder.Property(s => s.UnitCost)
                 .HasPrecision(18, 2)
                 .IsRequired();

            builder.Ignore(s => s.LineTotal);
                
        }
    }
}
