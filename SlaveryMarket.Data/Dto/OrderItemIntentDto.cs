namespace SlaveryMarket.Data.Dto;

public record OrderItemIntentDto(
    long ProductId, 
    decimal Price,
    float Amount);