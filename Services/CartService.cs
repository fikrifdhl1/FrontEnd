using BackendService.Models.DTO;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Services
{
    public interface ICartService
    {
        Task<ApiResponse<CartDTO>> GetCartByIdAsync(int id);
        Task<ApiResponse<List<CartDTO>>> GetAllCartsAsync();
        Task<ApiResponse<CartDTO>> CreateCartAsync(CreateCartDTO cart);
        //Task<ApiResponse<CartDTO>> UpdateCartAsync(int id, UpdateCartDTO cart);
        Task<ApiResponse<object>> DeleteCartAsync(int id);

        Task<ApiResponse<object>> AddItemToCartAsync(CreateCartItemDTO cartItem);
        Task<ApiResponse<CartItemDTO>> UpdateCartItemAsync(UpdateCartItemDTO cartItem);
        Task<ApiResponse<object>> DeleteCartItemAsync(int id);
        Task<ApiResponse<CartItemDTO>> GetCartItemByIdAsync(int id);
    }

    public class CartService : ICartService
    {
        private readonly HttpClient _httpClient;
        private readonly string _host;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartService(IHttpContextAccessor context,IConfiguration configuration)
        {
            _httpContextAccessor = context;
            _httpClient = new HttpClient();
            _host = configuration["RestAPI:URL"];
            var token = _httpContextAccessor.HttpContext.Session.GetString("sessionLogin-token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<ApiResponse<CartDTO>> GetCartByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync(_host + $"cart/{id}");
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<CartDTO>>(json);
        }

        public async Task<ApiResponse<List<CartDTO>>> GetAllCartsAsync()
        {
            var response = await _httpClient.GetAsync(_host + "cart");
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<List<CartDTO>>>(json);
        }

        public async Task<ApiResponse<CartDTO>> CreateCartAsync(CreateCartDTO cart)
        {
            var content = new StringContent(JsonConvert.SerializeObject(cart), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_host + "cart", content);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<CartDTO>>(json);
        }

        //public async Task<ApiResponse<CartDTO>> UpdateCartAsync(int id, UpdateCartDTO cart)
        //{
        //    var content = new StringContent(JsonConvert.SerializeObject(cart), Encoding.UTF8, "application/json");
        //    var response = await _httpClient.PutAsync(_host + $"cart/{id}", content);
        //    var json = await response.Content.ReadAsStringAsync();
        //    return JsonConvert.DeserializeObject<ApiResponse<CartDTO>>(json);
        //}

        public async Task<ApiResponse<object>> DeleteCartAsync(int id)
        {
            var response = await _httpClient.DeleteAsync(_host + $"cart/{id}");
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<object>>(json);
        }

        public async Task<ApiResponse<object>> AddItemToCartAsync(CreateCartItemDTO cartItem)
        {
            var content = new StringContent(JsonConvert.SerializeObject(cartItem), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_host + $"cart/{cartItem.CartId}/items", content);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<object>>(json);
        }

        public async Task<ApiResponse<CartItemDTO>> UpdateCartItemAsync(UpdateCartItemDTO cartItem)
        {
            var content = new StringContent(JsonConvert.SerializeObject(cartItem), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_host + $"cart/{cartItem.CartId}/items/{cartItem.Id}", content);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<CartItemDTO>>(json);
        }

        public async Task<ApiResponse<object>> DeleteCartItemAsync(int id)
        {
            var response = await _httpClient.DeleteAsync(_host + $"cart/items/{id}");
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<object>>(json);
        }

        public async Task<ApiResponse<CartItemDTO>> GetCartItemByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync(_host + $"cart/items/{id}");
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<CartItemDTO>>(json);
        }
    }
}
