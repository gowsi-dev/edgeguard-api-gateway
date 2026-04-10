using EdgeGuard.Application.Interfaces;
using EdgeGuard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdgeGuard.Application.Services
{
    public class RateLimitService:IRateLimitService
    {
        private readonly IRateLimitRepository _rateLimitService;
        public RateLimitService(IRateLimitRepository rateLimitService) {
            _rateLimitService = rateLimitService;
        }
        public Task<SubscriptionPlan?> GetSubscriptionPlan(int subscriptionId)
        {
            var plan =  _rateLimitService.GetSubscriptionPlan(subscriptionId);
            return plan;
        }
    }
}
