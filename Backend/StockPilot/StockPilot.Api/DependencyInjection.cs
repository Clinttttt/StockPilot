using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;

namespace StockPilot.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddAuthorization();    

            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Please enter a valid token",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    Name = "Authorization"

                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                 {
                     {
                         new OpenApiSecurityScheme
                         {
                         Reference = new OpenApiReference
                         {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                         }
                     },
                         new string[] { }
                    }

            });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAngular", opt =>
                {
                    opt.WithOrigins("http://localhost:4200");
                    opt.AllowAnyMethod();
                    opt.AllowAnyHeader();
                    opt.AllowCredentials();

                });

            });

            return services;




        }
    }
}
