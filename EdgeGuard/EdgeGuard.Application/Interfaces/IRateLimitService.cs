using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EdgeGuard.Domain.Entities;

namespace EdgeGuard.Application.Interfaces
{
    public interface IRateLimitService
    {
        public Task<SubscriptionPlan?> GetSubscriptionPlan(int subscriptionId);
    }
}
