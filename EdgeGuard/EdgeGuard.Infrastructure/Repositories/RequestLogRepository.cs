using Dapper;
using EdgeGuard.Application.Interfaces;
using EdgeGuard.Domain.Entities;
using EdgeGuard.Infrastructure.Database;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdgeGuard.Infrastructure.Repositories
{
    public class RequestLogRepository: IRequestLogRepository
    {
        private readonly DatabaseSetting _databaseSetting;
        public RequestLogRepository(IOptions<DatabaseSetting> options) { 
            _databaseSetting = options.Value;
        }
        public void SaveRequestAndResponseLog(RequestLog dto)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_databaseSetting.DefaultConnection))
                {
                    con.Execute("SP_SaveRequestResponseLog", new { dto.ClientId, dto.Endpoint, dto.Method, dto.StatusCode, dto.CreatedAt },
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
