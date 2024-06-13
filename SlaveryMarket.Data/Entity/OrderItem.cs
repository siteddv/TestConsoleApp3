namespace SlaveryMarket.Data.Model;

public class OrderItem
{
    public long Id { get; set; }
    public long ProductId { get; set; }
    public int Amount { get; set; }
}