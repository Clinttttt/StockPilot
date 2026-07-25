using StockPilot.Application.Common.Interfaces.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Infrastructure.Data
{
    public class UnitOfWork(IAppDbContext context) : IUnitOfWork
    {
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await context.SaveChangesAsync(cancellationToken);
    }
}
