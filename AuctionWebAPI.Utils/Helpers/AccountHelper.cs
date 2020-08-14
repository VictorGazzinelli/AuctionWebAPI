using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.DataHandler.Encoder;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AuctionWebAPI.Utils.Helpers
{
    public class AccountHelper
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public AccountHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static void SetHttpContextAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetAccountIdFromHttpContext()
        {
            return Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        public static int GetAccountIdFromJWT()
        {
            string authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            string tokenStr = authHeader.Substring("Bearer ".Length).Trim();
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.ReadToken(tokenStr) as JwtSecurityToken;
            Claim claim = token.Claims.FirstOrDefault(claim => string.Equals("nameid", claim.Type));

            return Convert.ToInt32(claim.Value);
        }

        public static string GetUsernameFromJWT()
        {
            string authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString();
            string tokenStr = authHeader.Substring("Bearer ".Length).Trim();
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            JwtSecurityToken token = handler.ReadToken(tokenStr) as JwtSecurityToken;
            Claim claim = token.Claims.FirstOrDefault(claim => string.Equals("unique_name", claim.Type));

            return claim.Value;
        }

        public static string CreateTokenForAccount(int accountId, string username)
        {
            const string sec = "qIYKsdwND-HtOITsX6dr9ncDqJVBE6-BvKNrIuQP_Pc";
            byte[] keyByteArray = TextEncodings.Base64Url.Decode(sec);
            var securityKey = new SymmetricSecurityKey(keyByteArray);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, accountId.ToString()),
                        new Claim(ClaimTypes.Name, username)
                    }
                ),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    securityKey,
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken);
        }

    }
}
