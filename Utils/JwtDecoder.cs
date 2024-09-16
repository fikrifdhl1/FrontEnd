using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace FrontEnd.Utils
{
    public class JwtDecoder
    {
        public static string GetJwtClaim(string token,string claim)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            var result = jwtToken.Claims.Where(x => x.Type == claim).FirstOrDefault();
            return result.Value;
        }
    }
}
