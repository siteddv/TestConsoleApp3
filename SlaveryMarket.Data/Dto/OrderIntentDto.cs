namespace SlaveryMarket.Data.Dto;

public record OrderIntentDto(
    long BuyerId,
    List<OrderItemIntentDto> OrderItems
);