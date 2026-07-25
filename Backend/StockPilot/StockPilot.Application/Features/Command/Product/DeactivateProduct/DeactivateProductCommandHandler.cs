using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Application.Features.Queries.Product.DeactivateProduct;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Product.DeactivateProduct
{
    public class DeactivateProductCommandHandler(IAppDbContext context, IUnitOfWork unitOfWork) : IRequestHandler<DeactivateProductCommand, Result>
    {
        public async Task<Result> Handle(DeactivateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await context.products.FirstOrDefaultAsync(s => s.Id == request.ProductId);

            if (product is null)
                return Result<bool>.NotFound("Product not found");
            if (product.IsActive is true)
            {
                product.IsActive = false;
            }
            else
            {
                product.IsActive = true;
            }
            context.products.Update(product);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result<bool>.Success(product.IsActive);
        }
    }
}
