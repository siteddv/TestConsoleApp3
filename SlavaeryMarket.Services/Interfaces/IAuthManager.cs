using SlaveryMarket.Data.Entity;
using SlaveryMarket.Web.Models;
using SlaveryMarket.Web.Requests;
using SlaveryMarket.Web.Responses;

namespace SlavaeryMarket.Services.Interfaces
{
    public interface IAuthManager<T>
        where T : ApplicationUser
    {
         Response<AuthResultStruct> Login(LoginModel model);

         Response<string> Register(RegiterModel model);
    }
}
