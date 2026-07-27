using MediatR;
using Microsoft.EntityFrameworkCore;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application.Features.Command.Product.DeleteProduct
{
    internal class DeleteProductCommandHandler(IAppDbContext context, IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductCommand, Result<bool>>
    {
        public async Task<Result<bool>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await context.products.FirstOrDefaultAsync(s => s.Id == request.ProductId);
            if (product is null) return Result<bool>.NotFound("Product not found");

            context.products.Remove(product);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<bool>.Success(true);

        }
    }
}
