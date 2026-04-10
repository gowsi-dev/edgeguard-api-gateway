using EdgeGuard.Application.DTOs;
using EdgeGuard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdgeGuard.Application.Interfaces
{
    public interface IRequestService
    {
        public void SaveRequestAndResponseLog(RequestLog dto) { }

    }
}
