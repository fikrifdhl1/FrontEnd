using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace WebApplication1.Models
{
    public class LoginResponse
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public Data Data { get; set; }
        public object ErrorDetails { get; set; }
    }

    public class Data
    {
        public string Token { get; set; }
    }
}
