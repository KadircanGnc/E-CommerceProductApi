using Blazored.SessionStorage;
using Common.DTOs.Brand;
using Common.DTOs.User;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace EcommerceClient.Infrastructure.Services
{
    public class BrandService
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorageService;
        public BrandService(HttpClient httpClient, ISessionStorageService sessionStorageService)
        {
            _httpClient = httpClient;
            _sessionStorageService = sessionStorageService;
        }

        private async Task<string> SetToken()
        {
            return await _sessionStorageService.GetItemAsync<string>("authToken");
        }

        public async Task<List<UpdateBrandDTO>> GetAll()
        {
            var token = await SetToken();
            var requestUri = "Brands";

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<UpdateBrandDTO>>();
                return result!;
            }
            else
            {
                throw new Exception("Failed to get brands.");
            }
        }

        public async Task<HttpResponseMessage> Create(CreateBrandDTO newBrand)
        {
            var token = await SetToken();
            var requestUri = "Brands/create";
            var content = new StringContent(JsonSerializer.Serialize(newBrand), Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = content
            };

            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to create brand");
            }
            return response;
        }

        public async Task<HttpResponseMessage> Update(UpdateBrandDTO editedBrand)
        {
            var token = await SetToken();
            var requestUri = "Brands/update-by-id";
            var content = new StringContent(JsonSerializer.Serialize(editedBrand), Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, requestUri)
            {
                Content = content
            };

            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to update brand");
            }
            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            var token = await SetToken();
            var requestUri = $"Brands/delete?id={id}";
            //var content = new StringContent(JsonSerializer.Serialize(id), Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri);

            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to delete brand");
            }
            return response;
        }
    }
}
