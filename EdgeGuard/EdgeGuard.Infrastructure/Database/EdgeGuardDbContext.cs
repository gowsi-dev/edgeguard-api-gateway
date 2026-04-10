using EdgeGuard.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdgeGuard.Infrastructure.Database
{
    public class EdgeGuardDbContext: DbContext
    {
        public EdgeGuardDbContext(DbContextOptions<EdgeGuardDbContext> options)
            : base(options)
        {
        }
        public DbSet<Client> Clients { get; set; }
        public DbSet<SubscriptionPlan> Subscriptions { get; set; }
        public DbSet<RequestLog> RequestLogs { get; set; }
    }
}
