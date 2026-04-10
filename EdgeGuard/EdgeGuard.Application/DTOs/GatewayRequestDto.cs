using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdgeGuard.Application.DTOs
{
    public class GatewayRequestDto
    {
        public string ApiKey { get; set; }
        public string Endpoint { get; set; }
        public string HttpMethod { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string Body { get; set; }
        public string QueryString { get; set; }
    }
}
