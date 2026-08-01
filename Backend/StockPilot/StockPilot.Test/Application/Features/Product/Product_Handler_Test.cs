using Microsoft.EntityFrameworkCore;
using Moq;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Application.Common.Interfaces.Services;
using StockPilot.Application.Features.Command.Product.AddProduct.cs;
using StockPilot.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Test.Application.Features.Product
{
    public class Product_Handler_Test
    {

        [Fact]
        public async Task HandleAsync_WithValidCommand_ShouldAdminUser()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseInMemoryDatabase(
            databaseName: Guid.NewGuid().ToString())
        .Options;
            using var ct = new CancellationTokenSource();
            CancellationToken cancellationToken = ct.Token;

            await using var context = new AppDbContext(options);
            var currentUser = new Mock<ICurrentUserService>();
            
            var handler = new AddProductCommandHandler(context, currentUser.Object);

            var command = new AddProductCommand(
                productName: "Macbook m4 max",
                sku: "mc-m4",
                CurrentStock: 15,
                MinimumStock: 10,
                ImageUrl: "wijjwewfwfee",
                CostPrice: 300000,
                Unit: "basta",
                CategoryId: Guid.Parse("8f2c7b1a-3d44-4e9f-9a6a-2c5e3f8d9b71")
                );

           await handler.Handle(command, cancellationToken);
           await context.SaveChangesAsync(cancellationToken);

            Assert.True(cancellationToken.IsCancellationRequested );
        
        }
    }
}


