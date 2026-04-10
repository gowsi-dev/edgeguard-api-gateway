using System.Collections.Concurrent;

namespace EdgeGuard.API.Middleware
{
    public class ClientRequestTracker
    {
        private readonly ConcurrentDictionary<string, (int Count, DateTime StartTime)> _clientData = new();

        public (int Count, DateTime StartTime) GetOrAdd(string clientId)
        {
            return _clientData.GetOrAdd(clientId, (0, DateTime.UtcNow));
        }

        public (int previousCount, int currentCount, DateTime startTime) IncrementCount(string clientId)
        {
            var clientData = _clientData.AddOrUpdate(
                clientId,
                (1, DateTime.UtcNow),  // If new: count=1, startTime=now
                (key, existing) => (existing.Count + 1, existing.StartTime) // Keep original startTime
            );

            // Get previous count (current - 1)
            var previousCount = clientData.Count - 1;

            return (previousCount, clientData.Count, clientData.StartTime);
        }

        public (int Count, DateTime StartTime) GetClientInfo(string clientId)
        {
            return _clientData.TryGetValue(clientId, out var data) ? data : (0, DateTime.MinValue);
        }
    }
}
