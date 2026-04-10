using EdgeGuard.Domain.Enums;

namespace EdgeGuard.Domain.Entities
{
    public class RequestLog
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public string Endpoint {  get; set; }
        public string Method { get; set; }
        public int StatusCode { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
