using System.Text.Json.Serialization;
using SlaveryMarket.Data.Model;

namespace SlaveryMarket.Data.Entity;

public class Client
{
    public Client(string name, decimal cashAmount, string phone)
    {
        Name = name;
        CashAmount = cashAmount;
        Phone = phone;
        BoughtProducts = new List<Product>();
    }
    
    public string Name { get; set; }
    public decimal CashAmount { get; set; }
    public string Phone { get; set; }
    [JsonIgnore]
    public List<Product> BoughtProducts { get; set; }
    
    public Janar Janar { get; set; } = new Janar("gay");
}