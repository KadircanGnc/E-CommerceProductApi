using Common.DTOs.Product;
using System.Net.Http;
using System.Net.Http.Headers;

namespace EcommerceClient.Infrastructure.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;
        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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

	}
}
