using EdgeGuard.Application.DTOs;
using EdgeGuard.Application.Interfaces;

namespace EdgeGuard.Application.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository) {
            _clientRepository = clientRepository;
        }
        public Task<ClientDto?> GetClient(string apikey) {
            var clients = _clientRepository.GetClientByApiKey(apikey);
            return clients;
        }

    }
}
