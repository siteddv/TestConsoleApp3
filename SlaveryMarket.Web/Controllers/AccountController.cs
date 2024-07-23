using Microsoft.AspNetCore.Mvc;
using SlaveryMarket.Web.Requests;
using SlaveryMarket.Web.Responses;

namespace SlaveryMarket.Web.Controllers;

public class AccountController : BaseController
{
    [HttpPost]
    public Response<object> SignUp([FromBody] SignUpRequest request)
    {
        return new Response<object>(null, "Sign up successfully", true);
    }
}