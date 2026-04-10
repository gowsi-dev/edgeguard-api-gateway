using EdgeGuard.Application.DTOs;
using EdgeGuard.Application.Interfaces;
using EdgeGuard.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Collections.Concurrent;

namespace EdgeGuard.API.Middleware
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        // In-memory store
        private static readonly ConcurrentDictionary<string, (int Count, DateTime StartTime)> _requestStore
            = new ConcurrentDictionary<string, (int, DateTime)>();
        public RateLimitMiddleware(RequestDelegate next)
        {
            _next = next;
            //_previous = previous;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var apiKey = context.Request.Headers["x-api-key"].ToString();

            var items = new Dictionary<string, object>();

            var logger = context.RequestServices.GetService(typeof(IRequestService)) as IRequestService;

            RequestLog requestLog = new RequestLog();
            requestLog.Method = context.Request.Method;
            requestLog.Endpoint = context.Request.Path;
            requestLog.CreatedAt = DateTime.UtcNow;

            requestLog.ClientId = apiKey.ToString();

            if (context.Items.Count == 0)
            {
                await context.Response.WriteAsync("No items in context");
            }
            ClientDto client = new ClientDto();
            // ✅ ALSO WORKS - Use KeyValuePair
            foreach (KeyValuePair<object, object> item in context.Items)
            {
                var key = item.Key;
                var value = item.Value;

                // Access nested Client
                if (key == "Client")
                {
                    client = value as ClientDto;
                }
            }

            //var client = items["Client"] as Client;
            if (client == null)
            {
                context.Response.StatusCode = 500;

                requestLog.StatusCode = context.Response.StatusCode;

                // Save request into log
                logger.SaveRequestAndResponseLog(requestLog);
                await context.Response.WriteAsync("Client not found in context");
                return;
            }
            var clientId = client.Id.ToString();

            // 2️⃣ Get rate limit rules
            var limit = client.subscriptionPlan.RequestLimit;
            var window = client.subscriptionPlan.TimeWindow; // in seconds

            var now = DateTime.UtcNow;
            context.Response.Headers.Add("X-RateLimit-Limit", limit.ToString());
            if (_requestStore.TryGetValue(clientId, out var entry))
            {
                var elapsed = (now - entry.StartTime).TotalSeconds;
                // within window
                var newCount = entry.Count + 1;

                context.Response.Headers.Add("X-RateLimit-Remaining", ((limit - newCount) >= 0 ? (limit - newCount) : 0).ToString());
                context.Response.Headers.Add("X-RateLimit-Reset", (window - elapsed).ToString());
                if (elapsed < window)
                {   
                    if (newCount > limit)
                    {
                        context.Response.StatusCode = 429;
                        requestLog.StatusCode = context.Response.StatusCode;

                        // Save request into log
                        logger.SaveRequestAndResponseLog(requestLog);
                        await context.Response.WriteAsync(string.Format("Limit exceeded. Try again after {0} seconds", window - elapsed));
                        return;
                    }

                    _requestStore[clientId] = (newCount, entry.StartTime);
                }
                else
                {
                    // window expired → reset
                    _requestStore[clientId] = (1, now);
                    context.Response.Headers.Add("X-RateLimit-Remaining", ((limit - 1) >= 0 ? (limit - 1) : 0).ToString());
                    context.Response.Headers.Add("X-RateLimit-Reset", (window - now.Second).ToString());
                }
            }
            else
            {
                // first request
                _requestStore[clientId] = (1, now);
                context.Response.Headers.Add("X-RateLimit-Remaining", ((limit - 1) >= 0 ? (limit - 1) : 0).ToString());
                context.Response.Headers.Add("X-RateLimit-Reset", (window - now.Second).ToString());
            }
            //var val = _previous.Invoke(context);
            await _next(context);
        }
    }
}
