using SlaveryMarket.Data.Dto;
using SlaveryMarket.Data.Repository;

namespace SlaveryMarket.BL.Controller;

public class OrderController
{
    private readonly OrderRepository _orderRepository;
    
    public OrderController()
    {
        _orderRepository = new OrderRepository();
    }
    
    public void BuyProduct(OrderIntentDto orderIntentDto)
    {
        _orderRepository.BuyProduct(orderIntentDto);
    }
    
}