using SlaveryMarket.Data.Entity;
using SlaveryMarket.Web.Models;
using SlaveryMarket.Web.Requests;
using SlaveryMarket.Web.Responses;

namespace SlavaeryMarket.Services.Interfaces
{
    public interface ITokenManager<T>
        where T : ApplicationUser
    {
        Response<AuthResultStruct> UpdateToken(TokenModel model);

        Response<bool> RevokeRefreshTokenByUsername(string username);

        Response<bool> RevokeAllRefreshToken();
    }
}
