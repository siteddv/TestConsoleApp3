using Microsoft.AspNetCore.Mvc;

namespace SlaveryMarket.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController
{
    [HttpGet]
    public string Get()
    {
        return "Hello from ProductController!";
    }
}