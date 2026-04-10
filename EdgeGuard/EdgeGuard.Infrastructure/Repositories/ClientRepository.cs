using Dapper;
using EdgeGuard.Application.DTOs;
using EdgeGuard.Application.Interfaces;
using EdgeGuard.Infrastructure.Database;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EdgeGuard.Infrastructure.Repositories
{
    public class ClientRepository:IClientRepository
    {
        private readonly DatabaseSetting _databaseSetting;
        public ClientRepository(IOptions<DatabaseSetting> options) { 
            _databaseSetting = options.Value;
        }
        public async Task<ClientDto?> GetClientByApiKey(string apikey)
        {
            ClientDto dto = new ClientDto();
            try
            {
                using(SqlConnection con = new SqlConnection(_databaseSetting.DefaultConnection))
                {

                    return await con.QueryFirstOrDefaultAsync<ClientDto>("SP_GetClient", new { apikey },
                                    commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {

            }
            return dto;
        }
    }
}
