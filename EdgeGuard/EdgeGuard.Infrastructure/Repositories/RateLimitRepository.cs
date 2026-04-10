using Dapper;
using EdgeGuard.Application.DTOs;
using EdgeGuard.Application.Interfaces;
using EdgeGuard.Domain.Entities;
using EdgeGuard.Infrastructure.Database;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdgeGuard.Infrastructure.Repositories
{
    public class RateLimitRepository:IRateLimitRepository
    {
        private readonly DatabaseSetting _databaseSetting;
        public RateLimitRepository(IOptions<DatabaseSetting> options) {
            _databaseSetting = options.Value;

        }

        public async Task<SubscriptionPlan?> GetSubscriptionPlan(int subscriptionId)
        {
            SubscriptionPlan dto = new SubscriptionPlan();
            try
            {
                using (SqlConnection con = new SqlConnection(_databaseSetting.DefaultConnection))

                {

                    return await con.QueryFirstOrDefaultAsync<SubscriptionPlan>("SP_GetSubscriptionPlan", new { subscriptionId },
                                    commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
    }
}
