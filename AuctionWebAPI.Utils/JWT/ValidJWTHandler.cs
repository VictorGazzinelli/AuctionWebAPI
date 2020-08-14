using AuctionWebAPI.Repositories.Game;
using AuctionWebAPI.Utils;
using AuctionWebAPI.Utils.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Utils
{
    public class ValidJWTHandler : AuthorizationHandler<ValidJWTRequirement>
    {
        IHttpContextAccessor _httpContextAccessor = null;
        public ValidJWTHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ValidJWTRequirement requirement)
        {
            string authHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            if (authHeader == null || !authHeader.StartsWith("Bearer"))
            {
                throw new UnauthorizedException("O Cabeçalho de autorização esta vazio ou não contem 'Bearer' ");
            }
            string jwt = authHeader.Substring("Bearer ".Length).Trim();
            if (ValidateToken(jwt))
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            context.Fail();
            return Task.CompletedTask;
        }

        private bool ValidateToken(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();
            SecurityToken validatedToken;
            try
            {
                ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(authToken, validationParameters, out validatedToken);
                int accountId = Convert.ToInt32(claimsPrincipal.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value);
                if (new AccountRepository().Retrieve(accountId) == null)
                    throw new Exception();
            }
            catch (Exception e)
            {
                throw new UnauthorizedException("Seu token de acesso não pode ser validado");
            }
            return validatedToken != null;
        }

        private TokenValidationParameters GetValidationParameters()
        {
            const string sec = "qIYKsdwND-HtOITsX6dr9ncDqJVBE6-BvKNrIuQP_Pc";
            byte[] keyByteArray = TextEncodings.Base64Url.Decode(sec);

            var securityKey = new SymmetricSecurityKey(keyByteArray);
            //var securityKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(sec));
            return new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKey = securityKey
            };
        }
    }
}
