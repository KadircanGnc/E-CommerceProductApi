using Blazored.SessionStorage;
using Common.DTOs.User;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;

namespace EcommerceClient.Infrastructure.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorageService;        
        public UserService(HttpClient httpClient, ISessionStorageService sessionStorageService)
        {
            _httpClient = httpClient;
            _sessionStorageService = sessionStorageService;            
        }

        private async Task<string> SetToken()
        {
            return await _sessionStorageService.GetItemAsync<string>("authToken");
        }

        public async Task<HttpResponseMessage> Create(CreateUserDTO newUser)
        {
            var token = await SetToken();
            var requestUri = "Users/create";
            var content = new StringContent(JsonSerializer.Serialize(newUser), Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = content
            };

            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);
            
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to create user");               
            }
            return response;
        }

        public async Task<HttpResponseMessage> Update(UpdateUserDTO editedUser)
        {
            var token = await SetToken();
            var requestUri = "Users/update-by-id";
            var content = new StringContent(JsonSerializer.Serialize(editedUser), Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, requestUri)
            {
                Content = content
            };

            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to update user");
            }
            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            var token = await SetToken();
            var requestUri = $"Users/delete?id={id}";
            //var content = new StringContent(JsonSerializer.Serialize(id), Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri);            

            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to delete user");
            }
            return response;
        }

        public async Task<List<UpdateUserDTO>> GetAll()
        {
            var token = await SetToken();
            var requestUri = "Users";

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<UpdateUserDTO>>();
                return result!;
            }
            else
            {
                throw new Exception("Failed to get users.");
            }
        }
    }
}
