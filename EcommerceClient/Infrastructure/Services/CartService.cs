using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;

namespace EcommerceClient.Infrastructure.Services
{
	public class CartService
	{
		private HttpClient _httpClient;
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
				return int.Parse(content); // Assuming the API returns the count as a plain integer
			}
			else
			{
				throw new Exception("Failed to get item count from cart.");
			}
		}

		
	}
}
