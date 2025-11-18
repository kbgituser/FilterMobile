using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Mobile.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        public string? JwtToken { get; private set; }
        public string? RefreshToken { get; private set; }
        public DateTime? ExpiresAt { get; private set; }

        public AuthService(string baseUrl)
        {
            _httpClient = new HttpClient();
            _baseUrl = baseUrl.TrimEnd('/') + "/auth";
        }

        private static readonly JsonSerializerOptions _jsonOptions = new(JsonSerializerDefaults.Web);

        public async Task<bool> LoginAsync(string email, string password)
        {
            var loginData = new { Email = email, Password = password };
            var json = JsonSerializer.Serialize(loginData, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/login", content);
            if (!response.IsSuccessStatusCode)
                return false;
            var responseJson = await response.Content.ReadAsStringAsync();
            var tokenObj = JsonSerializer.Deserialize<TokenResponse>(responseJson, _jsonOptions);
            if (tokenObj == null) return false;
            JwtToken = tokenObj.AccessToken;
            RefreshToken = tokenObj.RefreshToken;
            // Persist tokens
            Preferences.Set("AccessToken", JwtToken);
            Preferences.Set("RefreshToken", RefreshToken);
            return true;
        }

        public async Task<bool> RefreshAsync()
        {
            RefreshToken ??= Preferences.Get("RefreshToken", null);
            if (string.IsNullOrEmpty(RefreshToken)) return false;
            var payload = new { RefreshToken = RefreshToken };
            var json = JsonSerializer.Serialize(payload, _jsonOptions);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/refresh", content);
            if (!response.IsSuccessStatusCode) return false;
            var responseJson = await response.Content.ReadAsStringAsync();
            var tokenObj = JsonSerializer.Deserialize<TokenResponse>(responseJson, _jsonOptions);
            if (tokenObj == null) return false;
            JwtToken = tokenObj.AccessToken;
            RefreshToken = tokenObj.RefreshToken; // rotate
            Preferences.Set("AccessToken", JwtToken);
            Preferences.Set("RefreshToken", RefreshToken);
            return true;
        }

        public void AttachToken(HttpClient client)
        {
            JwtToken ??= Preferences.Get("AccessToken", null);
            if (!string.IsNullOrEmpty(JwtToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtToken);
            }
        }

        private class TokenResponse
        {
            public string AccessToken { get; set; } = string.Empty;
            public string RefreshToken { get; set; } = string.Empty;
        }
    }
}
