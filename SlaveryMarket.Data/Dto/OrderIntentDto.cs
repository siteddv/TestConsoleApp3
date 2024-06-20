namespace SlaveryMarket.Data.Dto;

public record OrderIntentDto
{
    public long BuyerId;
    public List<OrderItemIntentDto> OrderItems;
    public decimal TotalPrice
        => OrderItems.Sum(x => x.Price * (decimal)x.Amount);
}