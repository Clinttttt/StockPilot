using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.Features;
using System.Diagnostics;
using System.Text.Json;


namespace StockPilot.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

            }
            catch (ValidationException ex)
            {
                await ValidationExceptionHandler(context, ex);
            }
            catch (Exception ex)
            {
                await ExceptionHandler(context, ex);
            }
        }

        public static Task ExceptionHandler(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var response = new
            {
                type = "https://tools.ietf.org/html/rfc7231#section-6.6.1",
                message = "An unexpected error occurred.",
                title = "Internal Server Error",
                status = 500,
                error = exception.Message,
                detail = exception.StackTrace?.ToString()
            };
            return JsonSerializer.SerializeAsync(context.Response.Body, response);
        }



        public static Task ValidationExceptionHandler(HttpContext context, ValidationException exception)
        {
            context.Response.StatusCode = 400;
            context.Response.ContentType = "application/json";

            var error = exception.Errors.GroupBy(s => s.PropertyName)
                .ToDictionary(s => s.Key, s => s.Select(s => s.ErrorMessage).ToArray());

            var errorResponse = new
            {
                type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                message = "Validation failed",
                title = "One or more validation errors occurred.",
                status = 400,
                error
            };
            return JsonSerializer.SerializeAsync(context.Response.Body, errorResponse);
        }
    }
}
