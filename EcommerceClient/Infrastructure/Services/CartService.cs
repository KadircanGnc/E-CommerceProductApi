using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;
using Common.DTOs;

namespace EcommerceClient.Infrastructure.Services
{
	public class CartService
	{
		private HttpClient _httpClient;
        public event Func<Task<bool>>? isCartChanged;
		private int currentUserId = 0;
        public CartService(HttpClient httpClient) 
		{
			_httpClient = httpClient;  			
        }

		public async Task AddToCartAsync(string token, List<int> productIds)
		{
			var requestUri = "Carts/add-items";
			var content = new StringContent(JsonSerializer.Serialize(productIds), Encoding.UTF8, "application/json");

			var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
			{
				Content = content
			};

			requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await _httpClient.SendAsync(requestMessage);
			await IsCartChangedAsync();
			if (!response.IsSuccessStatusCode)
			{
				throw new Exception("Failed to add product to cart.");
			}
		}

		public async Task RemoveFromCartAsync(string token, List<int> productIds)
		{
			var requestUri = "Carts/remove-items";
			var content = new StringContent(JsonSerializer.Serialize(productIds), Encoding.UTF8, "application/json");

			var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
			{
				Content = content
			};

			requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await _httpClient.SendAsync(requestMessage);

			if (!response.IsSuccessStatusCode)
			{
				throw new Exception("Failed to remove product from cart.");
			}
		}

		public async Task<int> GetItemCountAsync(string token)
		{			
			var requestUri = "Carts/get-item-count";

			var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
			requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await _httpClient.SendAsync(requestMessage);

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				return int.Parse(content); 
			}
			else
			{
				throw new Exception("Failed to get item count from cart.");
			}
		}

		public async Task<List<int>> GetCartItemsAsync(string token)
		{
			var requestUri = "Carts/get-cart-items"; 

			var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
			requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

			var response = await _httpClient.SendAsync(requestMessage);
			if (response.IsSuccessStatusCode)
			{
				var result = await response.Content.ReadFromJsonAsync<List<int>>();
				return result!;
			}
			else
			{
				throw new Exception("Failed to get cart items.");
			}
		}

        public async Task<List<CartItemDTO>> GetCartDetailsAsync(string token)
        {
            var requestUri = "Carts/get-cart-items"; 
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<List<CartItemDTO>>();
                return result!;
            }
            else
            {
                throw new Exception("Failed to get cart details.");
            }
        }

        public async Task<bool> IsCartChangedAsync()
        {
            if (isCartChanged != null)
            {
                return await isCartChanged.Invoke();
            }
            return false;
        }

		public async Task<int> GetCurrentUserId()
		{
            var requestUri = "Users/get-current-user-id";
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var response = await _httpClient.SendAsync(requestMessage);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<int>();
                return result!;
            }
            else
            {
                throw new Exception("Failed to get user id.");
            }
        }

    }
}
