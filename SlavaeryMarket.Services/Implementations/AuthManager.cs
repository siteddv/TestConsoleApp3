﻿using Microsoft.AspNetCore.Identity;
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
using System.Security.Cryptography;
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
            try
            {
                if (model == null)
                {
                    throw new ArgumentNullException("model is null");
                }

                var user = new ApplicationUser
                {
                    Email = model.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = model.UserName,
                };

                var result = _userManager.CreateAsync((ApplicationUser?)user, model.Password);
                return
            }
            catch (ArgumentException ex)
            {

                return new Response<string>(null, ex.Message, false);
            }
            catch (Exception ex)
            {

                return new Response<string>(null, ex.Message, false);
            }
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
                
                return new Response<bool>(false, ex.Message, false);
                
            }
        }

        public Response<bool> RevokeRefreshTokenByUsername(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                {
                    throw new ArgumentNullException("username is null");
                }

                var user = _userManager.FindByNameAsync(username).Result;
                user.RefreshToken = null;
                _userManager.UpdateAsync(user);

                return new Response<bool>(true, null, true);
            }
            catch (ArgumentException ex)
            {
                return new Response<bool>(false, ex.Message, false);
            }
            catch (Exception ex)
            {
                return new Response<bool>(false, ex.Message, false);
            }
        }

        public Response<AuthResultStruct> UpdateToken(TokenModel model)
        {
            throw new NotImplementedException();
        }

        protected override string GenerateRefreshToken()
        {
            var randomNamber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNamber);
            return Convert.ToBase64String(randomNamber);
        }

        protected override JwtSecurityToken GenerateToken(List<Claim> claims)
        {
            if (claims == null)
            {
                throw new ArgumentNullException("claims is null");
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            bool isTokenValidityInt = int.TryParse(_configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            return new JwtSecurityToken
                (
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddMinutes(isTokenValidityInt ? tokenValidityInMinutes : 0),
                    claims: claims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
        }

        protected override ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string? token)
        {
            if (token == null || token == "") throw new ArgumentNullException("token is null");

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

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                 !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid Token");


            return principal;
        }
    }
}