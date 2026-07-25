using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Category
{
    internal class AddCategoryCommandHandler(IAppDbContext context, IUnitOfWork unitOfWork) : IRequestHandler<AddCategoryCommand, Result>
    {
        public async Task<Result> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            if (await context.Categories.AnyAsync(s => s.Name == request.name)) return Result<Guid>.Conflict("Category already exists");

            var entity = StockPilot.Domain.Entities.Category.Create(request.name, request.description);
            
            await context.Categories.AddAsync(entity);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success();
        }
    }
}
