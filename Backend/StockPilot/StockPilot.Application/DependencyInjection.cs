using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using StockPilot.Application.Behaviors;
using StockPilot.Application.Features.Command.Auth.Register;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPilot.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);

            services.AddMediatR(option =>
            {
                option.RegisterServicesFromAssembly(typeof(ApplicationAssemblyMarker).Assembly);
                option.AddOpenBehavior(typeof(ValidationBehaviors<,>));

            });



            return services;
        }
    }
}
