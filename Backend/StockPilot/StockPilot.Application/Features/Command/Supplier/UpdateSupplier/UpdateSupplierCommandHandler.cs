using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Supplier.UpdateSupplier
{
    public class UpdateSupplierCommandHandler(IAppDbContext context) : IRequestHandler<UpdateSupplierCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(UpdateSupplierCommand request, CancellationToken cancellationToken)
        {
            var user = await context.suppliers.FirstOrDefaultAsync(s=> s.Id == request.SupplierId);
            if (user is null) return Result<bool>.NotFound("Supplier not found ");

            user.Update(request.FullName, request.PhoneNumber, request.Email, request.Address);
            
            await context.SaveChangesAsync(cancellationToken);
            return Result<bool>.Success(true);
        }
    }
}
