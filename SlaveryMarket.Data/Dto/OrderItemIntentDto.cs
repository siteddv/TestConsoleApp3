namespace SlaveryMarket.Data.Dto;

/// <summary>
/// Описание товара в заказе, который планируется сделать
/// </summary>
/// <param name="ProductId">id продукта</param>
/// <param name="Amount">кол-во продуктов</param>
public record OrderItemIntentDto(
    long ProductId, 
    decimal Amount
);