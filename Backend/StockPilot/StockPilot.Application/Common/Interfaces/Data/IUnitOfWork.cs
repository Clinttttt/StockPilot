using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Common.Interfaces.Data
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
