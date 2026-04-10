using EdgeGuard.Application.Interfaces;
using EdgeGuard.Application.Services;
using EdgeGuard.Domain.Entities;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace EdgeGuard.API.Middleware
{
    public class ApiKeyMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var logger = context.RequestServices.GetService(typeof(IRequestService)) as IRequestService;

            RequestLog requestLog = new RequestLog();
            requestLog.Method = context.Request.Method;
            requestLog.Endpoint = context.Request.Path;
            requestLog.CreatedAt = DateTime.UtcNow;
            // 1️⃣ Read API Key from header
            if (!context.Request.Headers.TryGetValue("x-api-key", out var apiKey))
            {
                context.Response.StatusCode = 401;
                requestLog.StatusCode = context.Response.StatusCode;
                requestLog.ClientId = apiKey.ToString();

                // Save request into log
                logger.SaveRequestAndResponseLog(requestLog);
                await context.Response.WriteAsync("API Key is missing");
                return;
            }

            // 2️⃣ Get IClientService from DI
            var clientService = context.RequestServices.GetService(typeof(IClientService)) as IClientService;
            requestLog.ClientId = apiKey.ToString();
            if (clientService == null)
            {
                context.Response.StatusCode = 500;
                requestLog.StatusCode = 500;

                // Save request into log
                logger.SaveRequestAndResponseLog(requestLog);
                await context.Response.WriteAsync("Internal server error");
                return;
            }

            // 3️⃣ Get client using API key
            var client = await clientService.GetClient(apiKey);

            // 4️⃣ Validate client
            if (client == null || !client.IsActive)
            {
                context.Response.StatusCode = 401;
                requestLog.StatusCode = context.Response.StatusCode;

                // Save request into log
                logger.SaveRequestAndResponseLog(requestLog);
                await context.Response.WriteAsync("Unauthorized client");
                return;
            }
            // 2️⃣ Get IClientService from DI
            var subscriptonPlan = context.RequestServices.GetService(typeof(IRateLimitService)) as IRateLimitService;
            if(subscriptonPlan == null)
            {
                context.Response.StatusCode = 500;
                requestLog.StatusCode = context.Response.StatusCode;
                
                // Save request into log
                logger.SaveRequestAndResponseLog(requestLog);
                await context.Response.WriteAsync("Internal server error");
                return;
            }

            var plan = await subscriptonPlan.GetSubscriptionPlan(client.SubscriptionPlanId);
            client.subscriptionPlan = plan;

            // 5️⃣ Store client for next middleware
            context.Items["Client"] = client;
            requestLog.StatusCode = context.Response.StatusCode;
            // Save request into log
            logger.SaveRequestAndResponseLog(requestLog);
            // 6️⃣ Continue to next middleware
            await _next(context);
            requestLog.StatusCode = context.Response.StatusCode;
            // Save request into log
            logger.SaveRequestAndResponseLog(requestLog);
        }
    }
}