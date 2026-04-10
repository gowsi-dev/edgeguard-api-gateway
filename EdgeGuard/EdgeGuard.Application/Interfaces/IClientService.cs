using EdgeGuard.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdgeGuard.Application.Interfaces
{
    public interface IClientService
    {
       public Task<ClientDto?> GetClient(string apikey);
    }
}
