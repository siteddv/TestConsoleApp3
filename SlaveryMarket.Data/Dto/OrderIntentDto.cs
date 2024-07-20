using System.ComponentModel.DataAnnotations;

namespace SlaveryMarket.Data.Dto;

public class OrderIntentDto
{
    public OrderIntentDto(long buyerId, List<OrderItemIntentDto> orderItems)
    {
        BuyerId = buyerId;
        OrderItems = orderItems;
    }
    
    public long BuyerId { get; set; }
    public List<OrderItemIntentDto> OrderItems { get; set; }
}