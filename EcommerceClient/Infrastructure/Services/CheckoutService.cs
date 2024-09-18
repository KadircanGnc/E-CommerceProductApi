using Common.DTOs;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace EcommerceClient.Infrastructure.Services
{
    public class CheckoutService
    {
        private HttpClient _httpClient;
        public CheckoutService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> PlaceOrder(string token, CreditCardDTO creditCard)
        {
            var requestUri = "Orders/create";
            var content = new StringContent(JsonSerializer.Serialize(creditCard), Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = content
            };

            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);
            

            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }
    }
}
