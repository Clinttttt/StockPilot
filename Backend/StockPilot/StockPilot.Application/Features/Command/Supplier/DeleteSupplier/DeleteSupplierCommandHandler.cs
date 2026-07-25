using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Text;

namespace StockPilot.Application.Features.Command.Supplier.DeleteSupplier
{
    internal class DeleteSupplierCommandHandler(IAppDbContext context) : IRequestHandler<DeleteSupplierCommand, Result>
    {
        public async Task<Result> Handle(DeleteSupplierCommand request, CancellationToken cancellationToken)
        {
            var supplier = await context.suppliers
                .FirstOrDefaultAsync(s=> s.Id == request.SupplierId,cancellationToken);

            if (supplier is null) return Result.NotFound("Supplier not found");

            if(!await context.purchaseOrders.AnyAsync(s => s.Id == request.SupplierId,cancellationToken))
            {
                return Result.Failure("Supplier still has purchase orders");
            }

            context.suppliers.Remove(supplier);
            await context.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
