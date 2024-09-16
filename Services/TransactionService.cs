using BackendService.Models.DTO;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;


namespace FrontEnd.Services
{
    public interface ITransactionService
    {
        Task<ApiResponse<List<TransactionDTO>>> GetTransactionsAsync();
        Task<ApiResponse<TransactionDTO>> GetTransactionByIdAsync(int id);
        Task<ApiResponse<object>> CreateTransactionAsync(CreateTransactionDTO transaction);
    }
    public class TransactionService : ITransactionService
    {
        private readonly HttpClient _httpClient;
        private readonly string _host;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionService(IHttpContextAccessor context,IConfiguration configuration)
        {
            _httpContextAccessor = context;
            _httpClient = new HttpClient();
            _host = configuration["RestAPI:URL"];
            var token = _httpContextAccessor.HttpContext.Session.GetString("sessionLogin-token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<ApiResponse<List<TransactionDTO>>> GetTransactionsAsync()
        {
            var response = await _httpClient.GetAsync(_host + "transaction");
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<List<TransactionDTO>>>(json);
        }

        public async Task<ApiResponse<TransactionDTO>> GetTransactionByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync(_host + $"transaction/{id}");
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<TransactionDTO>>(json);
        }

        public async Task<ApiResponse<object>> CreateTransactionAsync(CreateTransactionDTO transaction)
        {
            var content = new StringContent(JsonConvert.SerializeObject(transaction), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(_host + "transaction", content);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<object>>(json);
        }
    }
}
