namespace EdgeGuard.Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ApiKey { get; set; }
        public int SubscriptionPlanId { get; set; }
        public bool IsActive { get; set; }
    }
}
