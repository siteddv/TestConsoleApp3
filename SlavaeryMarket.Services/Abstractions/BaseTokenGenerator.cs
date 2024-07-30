using SlaveryMarket.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SlavaeryMarket.Services.Abstractions
{
    public abstract class BaseTokenGenerator<T>
        where T : ApplicationUser
    {
        public BaseTokenGenerator() 
        { 

        }

        protected abstract JwtSecurityToken GenerateToken(List<Claim> claims);

        protected abstract string GenerateRefreshToken();

        protected abstract ClaimsPrincipal GetClaimsPrincipalFromExpiredToken(string? token);
    }
}
