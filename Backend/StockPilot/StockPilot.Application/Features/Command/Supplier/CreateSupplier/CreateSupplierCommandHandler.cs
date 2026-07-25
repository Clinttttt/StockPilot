using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Domain.Common;
using StockPilot.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Supplier.CreateSupplier
{
    public class CreateSupplierCommandHandler(IAppDbContext context) : IRequestHandler<CreateSupplierCommand, Result<Guid>>
    {
        public async Task<Result<Guid>> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
        {
            if (await context.suppliers.AnyAsync(s => s.FullName == request.FullName))
            {
                return Result<Guid>.Conflict("Suppliers already exist");
            }
            var entity = StockPilot.Domain.Entities.Supplier.Create(request.FullName,
                request.PhoneNumber,
                request.Email,
                request.Address);
            await context.suppliers.AddAsync(entity);
            await context.SaveChangesAsync(cancellationToken);
            return Result<Guid>.Success(entity.Id);
        }
    }
}
