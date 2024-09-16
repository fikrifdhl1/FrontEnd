using BackendService.Models.DTO;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using WebApplication1.Models;

namespace WebApplication1.Services
{

    public interface IAccountService
    {
        Task<LoginResponse> LoginAsync(string username, string password);
        Task<ApiResponse<List<UserDTO>>> GetUsers();

    }
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly string _host;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AccountService(IHttpContextAccessor context,IConfiguration configuration)
        {

            _httpContextAccessor = context;
            _httpClient = new HttpClient();
            _host = configuration["RestAPI:URL"];
            var token = _httpContextAccessor.HttpContext.Session.GetString("sessionLogin-token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<ApiResponse<List<UserDTO>>> GetUsers()
        {
            var response = await _httpClient.GetAsync(_host + "user");
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse<List<UserDTO>>>(json);
        }

        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            var request = new
            {
                Username = username,
                Password = password,
            };

            var jsonContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(_host + "user/Login", jsonContent);

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<LoginResponse>(await response.Content.ReadAsStringAsync());
            }

            return null;
        }

    }
}
