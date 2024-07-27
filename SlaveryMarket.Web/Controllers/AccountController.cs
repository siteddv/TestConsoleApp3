using Microsoft.AspNetCore.Mvc;
using SlaveryMarket.Web.Models;
using SlaveryMarket.Web.Requests;
using SlaveryMarket.Web.Responses;

namespace SlaveryMarket.Web.Controllers;

public class AccountController : BaseController
{

    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody] LoginModel loginModel)
    {
        var response = new Response<LoginModel>(null, "auth complete", true);

        if(response.Success)
        {
            return Ok(response);
        }

        return Unauthorized(response.Message);
    }

    [HttpPost]
    [Route("register")]
    public IActionResult Register([FromBody] RegiterModel regiterModel)
    {
        var response = new Response<RegiterModel>(null, "register complete", true);

        return Ok(response);
    }
}