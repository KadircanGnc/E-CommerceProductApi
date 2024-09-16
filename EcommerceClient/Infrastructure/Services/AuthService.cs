namespace EcommerceClient.Infrastructure.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AuthResult> LoginAsync(string email, string password)
        {
            var loginData = new { email, password };
            var response = await _httpClient.PostAsJsonAsync("Authentication", loginData);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<AuthResult>();
                return result!; // Assuming the token is returned in { token: "your-token" }
            }

            return null!; // Or handle failure case differently
        }

        public class AuthResult
        {
            public string? Token { get; set; }            
        }
    }
}
