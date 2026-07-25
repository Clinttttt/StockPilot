using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Supplier.SetSupplierStatus
{
    public class SetSupplierStatusCommandHandler(IAppDbContext context) : IRequestHandler<SetSupplierStatusCommand, Result>
    {
        public async Task<Result> Handle(SetSupplierStatusCommand request, CancellationToken cancellationToken)
        {
            var supplier = await context.suppliers.FirstOrDefaultAsync(s => s.Id == request.SupplierId);
            if (supplier is null) return Result.NotFound("Supplier not found");
            supplier?.SetStatus(request.IsActive);
            await context.SaveChangesAsync(cancellationToken);
            return Result.Success();

        }
    }
}
