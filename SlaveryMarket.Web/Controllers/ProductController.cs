using Microsoft.AspNetCore.Mvc;
using SlaveryMarket.Data.Dto;
using SlaveryMarket.Data.Entity;
using SlaveryMarket.Data.Model;
using SlaveryMarket.Data.Repository;
using SlaveryMarket.Web.Responses;

namespace SlaveryMarket.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductRepository _productRepository;
    private readonly OrderRepository _orderRepository;
    private readonly ClientRepository _clientRepository;

    public ProductController(ProductRepository productRepository, 
        OrderRepository orderRepository, 
        ClientRepository clientRepository)
    {
        _productRepository = productRepository;
        _orderRepository = orderRepository;
        _clientRepository = clientRepository;
    }

    [HttpGet]
    [Route("get-all")]
    public Response<List<Product>> GetAll()
    {
        var products =  _productRepository.GetAll();
        // var products = new List<Product>();
        if (!products.Any())
        {
            return new Response<List<Product>>(null, "No products found", false);
        }
        return new Response<List<Product>>(products, "Products successfully received", true);
    }
    
    [HttpGet]
    [Route("get-clients")]
    public Response<List<Client>> GetClients()
    {
        var clients =  _clientRepository.GetAll();
        if (!clients.Any())
        {
            return new Response<List<Client>>(null, "No clients found", false);
        }
        return new Response<List<Client>>(clients, "Clients successfully received", true);
    }
    
    [HttpPost]
    [Route("buy-product")]
    public void BuyProduct([FromBody] OrderIntentDto orderIntentDto)
    {
        _orderRepository.BuyProduct(orderIntentDto);
    }
}