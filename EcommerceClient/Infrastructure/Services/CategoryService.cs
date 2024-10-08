﻿using Blazored.SessionStorage;
using Common.DTOs.Category;
using Common.DTOs.User;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace EcommerceClient.Infrastructure.Services
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorageService;
        public CategoryService(HttpClient httpClient, ISessionStorageService sessionStorageService)
        {
            _httpClient = httpClient;
            _sessionStorageService = sessionStorageService;
        }

        private async Task<string> SetToken()
        {
            return await _sessionStorageService.GetItemAsync<string>("authToken");
        }

        public async Task<HttpResponseMessage> Create(CreateCategoryDTO newCategory)
        {
            var token = await SetToken();
            var requestUri = "Categorys/create";
            var content = new StringContent(JsonSerializer.Serialize(newCategory), Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = content
            };

            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to create category");
            }
            return response;
        }

        public async Task<HttpResponseMessage> Update(UpdateCategoryDTO editedCategory)
        {
            var token = await SetToken();
            var requestUri = "Categorys/update-by-id";
            var content = new StringContent(JsonSerializer.Serialize(editedCategory), Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, requestUri)
            {
                Content = content
            };

            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to update category");
            }
            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            var token = await SetToken();
            var requestUri = $"Categorys/delete?id={id}";
            //var content = new StringContent(JsonSerializer.Serialize(id), Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri);

            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to delete category");
            }
            return response;
        }

        public async Task<List<UpdateCategoryDTO>> GetCategories()
        {
            var response = await _httpClient.GetAsync("Categorys");

            if (response.IsSuccessStatusCode)
            {
                var categories = await response.Content.ReadFromJsonAsync<List<UpdateCategoryDTO>>();
                return categories ?? new List<UpdateCategoryDTO>();
            }
            else
            {
                // Handle unsuccessful response
                Console.WriteLine("Failed to fetch categories.");
                return new List<UpdateCategoryDTO>();
            }
        }
    }
}
