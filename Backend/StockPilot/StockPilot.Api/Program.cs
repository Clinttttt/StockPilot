using StockPilot.Api;
using StockPilot.Api.Extension;
using StockPilot.Api.Middleware;
using StockPilot.Application;
using StockPilot.Infrastructure;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddApi();
builder.ConfigureAuthentication();
builder.Services.AddHttpContextAccessor();
builder.Services.AddApiRateLimiter(builder.Configuration);


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
   app.UseSwagger();
   app.UseSwaggerUI();
}

app.UseCors("AllowAngular");

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();

app.UseRateLimiter();

app.UseAuthorization();

app.MapControllers();

app.Run();
