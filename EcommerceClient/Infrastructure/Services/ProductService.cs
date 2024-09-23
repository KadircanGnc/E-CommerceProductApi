using Blazored.SessionStorage;
using Common.DTOs;
using Common.DTOs.Product;
using Common.DTOs.User;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace EcommerceClient.Infrastructure.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorageService;
        public ProductService(HttpClient httpClient, ISessionStorageService sessionStorageService)
        {
            _httpClient = httpClient;
            _sessionStorageService = sessionStorageService;
        }

        private async Task<string> SetToken()
        {
            return await _sessionStorageService.GetItemAsync<string>("authToken");
        }

        public async Task<List<GetProductDTO>> GetProducts()
        {
			var response = await _httpClient.GetAsync("Products");
			
			if (response.IsSuccessStatusCode)
			{
				var products = await response.Content.ReadFromJsonAsync<List<GetProductDTO>>();
				return products ?? new List<GetProductDTO>();
			}
			else
			{
				// Handle unsuccessful response
				Console.WriteLine("Failed to fetch products.");
				return new List<GetProductDTO>();
			}			
		}

        public async Task<PagedResult<GetProductDTO>> GetPagedProducts(int pageNumber, int pageSize)
        {
            var response = await _httpClient.GetFromJsonAsync<PagedResult<GetProductDTO>>($"Products/paged?pageNumber={pageNumber}&pageSize={pageSize}");
            if (response == null)
            {
                throw new Exception("No products found.");
            }
            return response;
        }
        
        // Get products by category with pagination
        public async Task<PagedResult<GetProductDTO>> GetPagedProductsByCategory(int categoryId, int pageNumber, int pageSize)
        {
            var response = await _httpClient.GetFromJsonAsync<PagedResult<GetProductDTO>>(
                $"Products/by-category-id-paged?categoryId={categoryId}&pageNumber={pageNumber}&pageSize={pageSize}");
            if (response == null)
            {
                throw new Exception("No products found for this category.");
            }
            return response;
        }

        // Search products with pagination
        public async Task<PagedResult<GetProductDTO>> SearchProductsWithPagination(string searchTerm, int pageNumber, int pageSize)
        {
            var response = await _httpClient.GetFromJsonAsync<PagedResult<GetProductDTO>>(
                $"Products/search-by-name-paged?name={searchTerm}&pageNumber={pageNumber}&pageSize={pageSize}");
            if (response == null)
            {
                throw new Exception("No products found for the search term.");
            }
            return response;
        }

        public async Task<GetProductDTO> GetById(int productId)
        {
            var response = await _httpClient.GetFromJsonAsync<GetProductDTO>($"Products/by-id?id={productId}");
            return response ?? new GetProductDTO();
        }

        public async Task<List<GetProductDTO>> GetProductsByCategoryId(int categoryId)
        {
            var response = await _httpClient.GetFromJsonAsync<List<GetProductDTO>>($"Products/by-category-id?categoryId={categoryId}");
            return response ?? new List<GetProductDTO>();
        }

        public async Task<HttpResponseMessage> Create(CreateProductDTO newProduct)
        {
            var token = await SetToken();
            var requestUri = "Products/create";
            var content = new StringContent(JsonSerializer.Serialize(newProduct), Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = content
            };

            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to create product");
            }
            return response;
        }

        public async Task<HttpResponseMessage> Update(UpdateProductDTO editedProduct)
        {
            var token = await SetToken();
            var requestUri = "Products/update-by-id";
            var content = new StringContent(JsonSerializer.Serialize(editedProduct), Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(HttpMethod.Put, requestUri)
            {
                Content = content
            };

            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to update product");
            }
            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            var token = await SetToken();
            var requestUri = $"Products/delete?id={id}";
            //var content = new StringContent(JsonSerializer.Serialize(id), Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, requestUri);

            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to delete product");
            }
            return response;
        }

        public async Task<List<UpdateProductDTO>> GetAllManage()
        {
            var token = await SetToken();
            var requestUri = "Products";

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<UpdateProductDTO>>();
                return result!;
            }
            else
            {
                throw new Exception("Failed to get products.");
            }
        }

    }
}
