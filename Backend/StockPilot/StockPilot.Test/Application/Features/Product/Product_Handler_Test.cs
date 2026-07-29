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

            await using var context = new AppDbContext(options);
            var currentUser = new Mock<ICurrentUserService>();
            
            var handler = new AddProductCommandHandler(context);

        }
    }
}


