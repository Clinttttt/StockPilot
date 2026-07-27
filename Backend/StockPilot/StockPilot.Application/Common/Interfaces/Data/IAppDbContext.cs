using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using StockPilot.Domain.Entities;
using StockPilot.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace StockPilot.Application.Common.Interfaces.Data
{
    public interface IAppDbContext
    {
        DbSet<Category> Categories { get; }
        DbSet <BaseUser> baseUsers { get; }
        DbSet<Supplier> suppliers { get; }
        DbSet<Product> products { get; }
        DbSet<PurchaseOrder> purchaseOrders { get; }
        DbSet<PurchaseOrderItem> purchaseOrderItems { get; }
        DbSet<StockMovement> stocksMovements { get; }
        DatabaseFacade Database { get; }

        Task<IDbContextTransaction> BeginTransactionAsync(
            CancellationToken cancellationToken = default);

        Task<IDbContextTransaction> BeginTransactionAsync(
            IsolationLevel isolationLevel,
            CancellationToken cancellationToken = default);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
