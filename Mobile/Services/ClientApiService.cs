using SharedModels.ClientDtos;
using System.Net.Http.Json;

namespace Mobile.Services
{
    public class ClientApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _clientEndpointUrl = "clients";
        private readonly AuthService? _authService;

        public ClientApiService(string baseUrl, AuthService? authService = null)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(baseUrl) };
            _authService = authService;
        }

        private void EnsureAuth()
        {
            _authService?.AttachToken(_httpClient);
        }

        public async Task<List<ClientDto>> GetClientsAsync()
        {
            EnsureAuth();
            return await _httpClient.GetFromJsonAsync<List<ClientDto>>(_clientEndpointUrl);
        }

        public async Task<ClientDto?> GetClientByIdAsync(int id)
        {
            EnsureAuth();
            return await _httpClient.GetFromJsonAsync<ClientDto>($"{_clientEndpointUrl}/{id}");
        }

        public async Task<bool> CreateClientAsync(ClientDto client)
        {
            EnsureAuth();
            var response = await _httpClient.PostAsJsonAsync(_clientEndpointUrl, client);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateClientAsync(int id, ClientDto client)
        {
            EnsureAuth();
            var response = await _httpClient.PutAsJsonAsync($"{_clientEndpointUrl}/{id}", client);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            EnsureAuth();
            var response = await _httpClient.DeleteAsync($"{_clientEndpointUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
