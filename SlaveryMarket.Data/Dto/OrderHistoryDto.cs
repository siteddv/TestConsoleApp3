namespace SlaveryMarket.Data.Dto;

public record OrderHistoryDto(
    long orderId, 
    string productName, 
    double productAmount, 
    decimal productPrice, 
    DateTime createdDate);