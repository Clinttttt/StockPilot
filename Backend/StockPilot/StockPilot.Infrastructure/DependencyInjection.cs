using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockPilot.Application.Common.Interfaces.Data;
using StockPilot.Application.Common.Interfaces.Services;
using StockPilot.Infrastructure.Data;
using StockPilot.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;





namespace StockPilot.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
      
            services.AddDbContext<Data.AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

  
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IAppDbContext,AppDbContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ITokenServices, TokenServices>();
            return services;
        }
    }
}
