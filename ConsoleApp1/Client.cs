namespace ConsoleApp1;

public class Client
{
    public Client(string name, decimal cashAmount, string gender)
    {
        Name = name;
        CashAmount = cashAmount;
        Gender = gender;
        BoughtProducts = new List<Product>();
    }
    
    public string Name;
    public decimal CashAmount;
    public string Gender;
    public List<Product> BoughtProducts;
    
    public void BuyProduct(Product product)
    {
        BoughtProducts.Add(product);
        CashAmount -= product.Price;
        Console.WriteLine("You bought a product!");
    }
}