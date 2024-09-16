using BackendService.Models.DTO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace FrontEnd.Services
{
    public interface IProductService
    {
        Task<ApiResponse<List<ProductDTO>>> GetProductsAsync();
        Task<ApiResponse<ProductDTO>> GetProductByIdAsync(int id);
        Task<ApiResponse<ProductDTO>> CreateProductAsync(CreateProductDTO product);
        Task<ApiResponse<ProductDTO>> UpdateProductAsync(int id, UpdateProductDTO product);
        Task<ApiResponse<object>> DeleteProductAsync(int id);
    }
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly string _host;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductService(IHttpContextAccessor context,IConfiguration configuration)
        {

            _httpContextAccessor = context;
            _httpClient = new HttpClient();
            _host = configuration["RestAPI:URL"];
            var token = _httpContextAccessor.HttpContext.Session.GetString("sessionLogin-token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<ApiResponse<List<ProductDTO>>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync(_host +"product");
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<List<ProductDTO>>>(json);
        }

        public async Task<ApiResponse<ProductDTO>> GetProductByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync(_host + $"product/{id}");
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<ProductDTO>>(json);
        }

        public async Task<ApiResponse<ProductDTO>> CreateProductAsync(CreateProductDTO product)
        {
            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_host + "product", content);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<ProductDTO>>(json);
        }

        public async Task<ApiResponse<ProductDTO>> UpdateProductAsync(int id, UpdateProductDTO product)
        {
            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(_host + $"product/{id}", content);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<ProductDTO>>(json);
        }

        public async Task<ApiResponse<object>> DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync(_host + $"product/{id}");
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<object>>(json);
        }
    }
}
