using Common.DTOs.Comment;

namespace EcommerceClient.Infrastructure.Services
{
    public class CommentService
    {
        private readonly HttpClient _httpClient;
        public CommentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GetCommentDTO>> GetByProductId(int productId)
        {
            var response = await _httpClient.GetFromJsonAsync<List<GetCommentDTO>>($"Comments/by-product-id?productId={productId}");
            return response ?? new List<GetCommentDTO>();
        }
    }
}
