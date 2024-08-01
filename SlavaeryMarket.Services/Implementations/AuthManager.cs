using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SlavaeryMarket.Services.Abstractions;
using SlavaeryMarket.Services.Interfaces;
using SlaveryMarket.Data.Entity;
using SlaveryMarket.Web.Models;
using SlaveryMarket.Web.Requests;
using SlaveryMarket.Web.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SlavaeryMarket.Services.Implementations
{
    public class AuthManager<T> : BaseTokenGenerator<T>, IAuthManager<T>, ITokenManager<T>
        where T : ApplicationUser
    {
        private readonly UserManager<T> _userManager;
        private readonly IConfiguration _configuration;

        public AuthManager(UserManager<T> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public Response<AuthResultStruct> Login(LoginModel model)
        {
            throw new NotImplementedException();
        }

        public Response<string> Register(RegiterModel model)
        {
            throw new NotImplementedException();
        }

        public Response<bool> RevokeAllRefreshToken()
        {
            try
            {
                var users = _userManager.Users.ToList();
                foreach (var user in users)
                {
                    user.RefreshToken = null;
                    _userManager.UpdateAsync(user);

                }
                return new Response<bool>(true, null, true);
            }
            catch (ArgumentException ex)
            {

                return new Response<bool>(false, ex.Message, false);
            }
            catch (Exception ex)
            {
                {
                    return new Response<bool>(false, ex.Message, false);
                }
            }
        }

        public Response<bool> RevokeRefreshTokenByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Response<AuthResultStruct> UpdateToken(TokenModel model)
        {
            throw new NotImplementedException();
        }

        protected override string GenerateRefreshToken()
        {
            throw new NotImplementedException();
        }

        protected override JwtSecurityToken GenerateToken(List<Claim> claims)
        {
            throw new NotImplementedException();
        }

        protected override ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string? token)
        {
            //VALIDATION
            var tokenValidationParams = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParams, out SecurityToken securityToken);

            if(securityToken is not JwtSecurityToken jwtSecurityToken
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid Token");


            return principal;
        }
    }
}
