using Common.DTOs.Product;

namespace EcommerceClient.Infrastructure.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        
    }
}
