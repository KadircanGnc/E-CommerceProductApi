using Common.DTOs.Category;

namespace EcommerceClient.Infrastructure.Services
{
    public class CategoryService
    {
        private readonly HttpClient _httpClient;
        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GetCategoryDTO>> GetCategories()
        {
            var response = await _httpClient.GetAsync("Categorys");

            if (response.IsSuccessStatusCode)
            {
                var categories = await response.Content.ReadFromJsonAsync<List<GetCategoryDTO>>();
                return categories ?? new List<GetCategoryDTO>();
            }
            else
            {
                // Handle unsuccessful response
                Console.WriteLine("Failed to fetch categories.");
                return new List<GetCategoryDTO>();
            }
        }
    }
}
