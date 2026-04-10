using EdgeGuard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdgeGuard.Application.Interfaces
{
    public interface IRateLimitRepository
    {
        public Task<SubscriptionPlan?> GetSubscriptionPlan(int subscriptionId);
    }
}
