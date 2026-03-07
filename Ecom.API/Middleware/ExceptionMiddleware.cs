using Ecom.API.Helper;
using Microsoft.Extensions.Caching.Memory;
using System.Net;
using System.Text.Json;

namespace Ecom.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public IHostEnvironment Environment { get; }
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _rateLimitWindow = TimeSpan.FromSeconds(30);

        public ExceptionMiddleware(RequestDelegate next, IHostEnvironment environment, IMemoryCache memoryCache)
        {
            _next = next;
            Environment = environment;
            _memoryCache = memoryCache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                ApplysecurityHeaders(context);

                if (!IsRequestAllowed(context))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    context.Response.ContentType = "application/json";

                    var response = new 
                        ApiException((int)HttpStatusCode.TooManyRequests, "Too many requests. Please try again later.");

                    var json = JsonSerializer.Serialize(response);

                    await context.Response.WriteAsync(json);
                    return;
                }
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = Environment.IsDevelopment() ?
                    new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace!)
                    : new ApiException((int)HttpStatusCode.InternalServerError, ex.Message);

                var json = JsonSerializer.Serialize(response);

                await context.Response.WriteAsync(json);
            }
        }

        private bool IsRequestAllowed(HttpContext context)
        {
            var ip = context.Connection.RemoteIpAddress?.ToString();
            var cacheKey = $"Rate:{ip}";
            var dateNow = DateTime.UtcNow;

            var (timesTamp, count) = _memoryCache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _rateLimitWindow;
                return (timesTamp: dateNow, count: 0);
            });

            if (dateNow - timesTamp < _rateLimitWindow)
            {
                if (count >= 5) // Limit to 5 requests per window
                {
                    return false;
                }
                _memoryCache.Set(cacheKey, (timesTamp, count += 1), _rateLimitWindow);
            }
            else
            {
                _memoryCache.Set(cacheKey, (timesTamp, count), _rateLimitWindow);
            }
            return true;
        }

        private void ApplysecurityHeaders(HttpContext context)
        {
            context.Response.Headers["X-Content-Type-Options"] = "nosniff";
            context.Response.Headers["X-Frame-Options"] = "DENY";
            context.Response.Headers["X-XSS-Protection"] = "1; mode=block";
            context.Response.Headers["Content-Security-Policy"] = "default-src 'self'";
        }
    }
}
