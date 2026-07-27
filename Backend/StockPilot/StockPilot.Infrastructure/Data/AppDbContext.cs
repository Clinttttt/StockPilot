using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Domain.Entities;
using StockPilot.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Text;

namespace StockPilot.Infrastructure.Data
{
    public class AppDbContext : DbContext , IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<BaseUser> baseUsers => Set<BaseUser>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Supplier> suppliers => Set<Supplier>();
        public DbSet<Product> products => Set<Product>();
        public DbSet<PurchaseOrder> purchaseOrders => Set<PurchaseOrder>();
        public DbSet<PurchaseOrderItem> purchaseOrderItems => Set<PurchaseOrderItem>();
        public DbSet<StockMovement> stocksMovements => Set<StockMovement>();

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return Database.BeginTransactionAsync(cancellationToken);
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(IsolationLevel isolationLevel, CancellationToken cancellationToken = default)
        {
            return Database.BeginTransactionAsync(isolationLevel, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<Product>()
                .HasQueryFilter(s => !s.IsDeleted && s.IsActive);
        }
    }
}
