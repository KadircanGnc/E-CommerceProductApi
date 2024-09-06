using Common.DTOs.Product;
using System.Net.Http;

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
            var products = await _httpClient.GetFromJsonAsync<List<GetProductDTO>>("Products");
            return products!;
        }
    }
}
