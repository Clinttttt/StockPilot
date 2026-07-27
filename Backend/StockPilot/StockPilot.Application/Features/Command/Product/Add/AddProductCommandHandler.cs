using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Application.Common.Interfaces.Services;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using static StockPilot.Domain.Entities.Enums;

namespace StockPilot.Application.Features.Command.Product.AddProduct.cs
{
    public class AddProductCommandHandler(IAppDbContext context,ICurrentUserService currentUser) : IRequestHandler<AddProductCommand, Result>
    {
        public async Task<Result> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            if(currentUser.Role != UserRole.Admin.ToString()) Result<bool>.Unauthorized("your not admin");

            if (await context.products.AnyAsync(s => s.Name == request.productName))
            {
                return Result<bool>.Conflict("Product Already Exist");
            }
           var entity = StockPilot.Domain.Entities.Product.Create
                (request.productName,request.sku, request.CurrentStock,
                request.MinimumStock,request.ImageUrl,request.CostPrice
                ,request.Unit,request.CategoryId);

            await context.products.AddAsync(entity);
            await context.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);
        }
    }
}
