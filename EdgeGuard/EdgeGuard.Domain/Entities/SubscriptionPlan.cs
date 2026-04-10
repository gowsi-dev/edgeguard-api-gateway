namespace EdgeGuard.Domain.Entities
{
    public class SubscriptionPlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RequestLimit { get; set; }
        public int TimeWindow { get; set; }
    }
}
