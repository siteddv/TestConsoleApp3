using Microsoft.AspNetCore.Mvc;

namespace SlaveryMarket.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    public string GetId()
    {
        return "123442432";
    }
}