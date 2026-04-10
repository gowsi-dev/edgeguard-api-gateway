using EdgeGuard.Application.DTOs;
using EdgeGuard.Application.Interfaces;
using EdgeGuard.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdgeGuard.Application.Services
{
    public class RequestService : IRequestService
    {
        private readonly IRequestLogRepository _requestLogRepository;
        public RequestService(IRequestLogRepository requestLogRepository)
        {
            _requestLogRepository = requestLogRepository;
        }
        public void SaveRequestAndResponseLog(RequestLog dto)
        {
            _requestLogRepository.SaveRequestAndResponseLog(dto);
        }
    }
}
