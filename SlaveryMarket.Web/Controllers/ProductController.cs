using Microsoft.AspNetCore.Mvc;
using SlaveryMarket.Data.Dto;
using SlaveryMarket.Data.Model;
using SlaveryMarket.Data.Repository;

namespace SlaveryMarket.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductRepository _productRepository;
    private readonly OrderRepository _orderRepository;

    public ProductController(ProductRepository productRepository, OrderRepository orderRepository)
    {
        _productRepository = productRepository;
        _orderRepository = orderRepository;
    }

    [HttpGet]
    [Route("get-all")]
    public List<Product> GetAll()
    {
        Console.WriteLine(_productRepository.Sanjar);
        return _productRepository.GetAll();
    }
    
    [HttpPost]
    [Route("buy-product")]
    public void BuyProduct([FromBody] OrderIntentDto orderIntentDto)
    {
        _orderRepository.BuyProduct(orderIntentDto);
    }
}