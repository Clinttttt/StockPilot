using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using StockPilot.Api.RateLimiting;
using System.Globalization;
using System.Security.Claims;
using System.Threading.RateLimiting;

namespace StockPilot.Api.Extension
{
    public static class RateLimiterExtension
    {
        public static IServiceCollection AddApiRateLimiter(
            this IServiceCollection services, IConfiguration configuration)
        {
            var generalPermitLimit = configuration.GetValue<int>("RateLimiting:General:" +
                "Permitlimit");
            var generalWindowMinutes = configuration.GetValue<int>("RateLimiting:General:" +
                "WindowMinutes");
            var loginPermitLimit = configuration.GetValue<int>("RateLimiting:Login:" +
                "PermitLimit");
            var loginWindowMinutes = configuration.GetValue<int>("RateLimiting:Login:" +
                "WindowMinutes");
            var sensitivePermitLimit = configuration.GetValue<int>("RateLimiting:" +
                "Sensitive:PermitLimit");
            var sensitiveWindowMinutes = configuration.GetValue<int>("RateLimiting:" +
                "Sensitive:WindowMinutes");
            var uploadPermitLimit = configuration.GetValue<int>("RateLimiting:" +
                "Upload:PermitLimit");
            var uploadQueueLimit = configuration.GetValue<int>("RateLimiting:" +
                "Upload:QueueLimit");

            services.AddRateLimiter(options =>
            {

                options.RejectionStatusCode =
                StatusCodes.Status429TooManyRequests;

                AddGeneralPolicy(
                    options,
                    generalPermitLimit,
                    generalWindowMinutes
                    );
                AddLoginPolicy(
                    options,
                    loginPermitLimit,
                    loginWindowMinutes
                    );
                AddSensitivePolicy(
                    options,
                    sensitivePermitLimit,
                    sensitiveWindowMinutes
                    );
                AddUploadPolicy(
                    options,
                    uploadPermitLimit,
                    uploadQueueLimit
                    );

                options.OnRejected = HandleRejectedRequestAsync;

            });


            return services;
        }
        public static void AddGeneralPolicy(RateLimiterOptions options,
            int permitLimit,
            int windowMinutes)
        {
            options.AddPolicy(
                RateLimitPolicies.General, context =>
                {
                    var clientKey = GetClientKey(context);
                    return RateLimitPartition.GetSlidingWindowLimiter(
                        clientKey,
                         _ => CreateSlidingWindowOptions(
                         permitLimit,
                         windowMinutes));
                });
        }

        public static void AddLoginPolicy(
            RateLimiterOptions options,
            int permitLimit,
            int windowMinutes
            )
        {
            options.AddPolicy(
                RateLimitPolicies.Login, context =>
                {
                    var ipKey = GetIpKey(context);
                    return RateLimitPartition.GetSlidingWindowLimiter(
                        ipKey,
                        _ => CreateSlidingWindowOptions(
                            permitLimit,
                            windowMinutes)
                        );});}

        public static void AddSensitivePolicy(
            RateLimiterOptions options,
            int permitLimit,
            int windowsMinutes
            ){
            options.AddPolicy(
                RateLimitPolicies.Sensitive,
                context =>
                {
                    var ipKey = GetIpKey(context);
                    return RateLimitPartition.GetSlidingWindowLimiter(
                        ipKey,
                        _ => CreateSlidingWindowOptions(
                            permitLimit,
                            windowsMinutes)
                        );});}

        public static void AddUploadPolicy(
            RateLimiterOptions options,
            int permitLimit,
            int windowsMinutes
            )
        {
            options.AddPolicy(
                RateLimitPolicies.Upload,
                context =>
                {
                    var clientKey = GetClientKey(context);
                    return RateLimitPartition.GetSlidingWindowLimiter(
                        clientKey,
                        _ => CreateSlidingWindowOptions(
                            permitLimit,
                            windowsMinutes
                            ));});}


        public static SlidingWindowRateLimiterOptions
            CreateSlidingWindowOptions(int permitLimit,
            int windowMinutes)
        {
            return new SlidingWindowRateLimiterOptions
            {
                PermitLimit = permitLimit,
                Window = TimeSpan.FromMinutes(windowMinutes),
                SegmentsPerWindow = 6,
                AutoReplenishment = true,
                QueueLimit = 0,
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst
            };
        }

        public static async ValueTask HandleRejectedRequestAsync(
            OnRejectedContext rejectedContext,
            CancellationToken cancellationToken
            )
        {
            var context = rejectedContext.HttpContext;

            int? retryAfterSeconds = null;

            if(rejectedContext.Lease.TryGetMetadata(
                MetadataName.RetryAfter,
                out var retryAfter
                ))
            {
                retryAfterSeconds = Math.Max(
                    1, (int)Math.Ceiling(retryAfter.TotalSeconds));

                context.Response.Headers.RetryAfter =
                    retryAfterSeconds.Value.ToString(
                        CultureInfo.InvariantCulture
                        );


            }

            var logger = context.RequestServices
                .GetRequiredService<ILoggerFactory>()
                .CreateLogger("RateLimiter");
            

            logger.LogWarning(
                      "Rate limit exceeded. Client: {Client}, Method: {Method}, Path: {Path}, TraceId: {TraceId}",
                      GetClientKey(context),
                      context.Request.Method,
                      context.Request.Path,
                      context.TraceIdentifier);

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(
                new {

                    status = StatusCodes.Status429TooManyRequests,
                    title = "Too Many Requests",
                    detail =
                    "Too many requests were sent. Please try again later.",
                    retryAfterSeconds,
                    traceId = context.TraceIdentifier
                }, cancellationToken);

        }






        public static string GetClientKey(HttpContext context)
        {
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? context.User.FindFirstValue("sub");
            if (!string.IsNullOrWhiteSpace(userId))
            {
                return $"user:{userId}";
            }
            return GetIpKey(context);
        }

        public static string GetIpKey(HttpContext context)
        {
            var ipAddress =
                context.Connection.RemoteIpAddress?.ToString()
                ?? "unkown";
            return $"ip:{ipAddress}";
        }
    }
}
