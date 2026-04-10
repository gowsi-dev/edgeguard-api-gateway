using EdgeGuard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdgeGuard.Application.DTOs
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubscriptionPlanId {  get; set; }
        public bool IsActive {  get; set; }
        public SubscriptionPlan subscriptionPlan { get; set; }
    }
}
