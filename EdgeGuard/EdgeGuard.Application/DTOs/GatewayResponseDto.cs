using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdgeGuard.Application.DTOs
{
    public class GatewayResponseDto
    {
        public int StatusCode { get; set; }
        public string ResponseBody { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
