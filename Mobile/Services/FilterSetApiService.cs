using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using SharedModels.FilterSetDtos;

namespace Mobile.Services
{
    public class FilterSetApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public FilterSetApiService(string baseUrl, string token)
        {
            _baseUrl = baseUrl;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<List<FilterSetDto>> GetFilterSetsAsync()
        {
            var response = await _httpClient.GetAsync(_baseUrl);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<FilterSetDto>>(json);
        }

        public async Task<FilterSetDto> GetFilterSetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/{id}");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<FilterSetDto>(json);
        }

        public async Task<bool> CreateFilterSetAsync(FilterSetDto filterSet)
        {
            var json = JsonSerializer.Serialize(filterSet);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_baseUrl, content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateFilterSetAsync(int id, FilterSetDto filterSet)
        {
            var json = JsonSerializer.Serialize(filterSet);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"{_baseUrl}/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteFilterSetAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_baseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
