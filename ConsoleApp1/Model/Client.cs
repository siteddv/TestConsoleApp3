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

    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}\n" +
               $"{nameof(CashAmount)}: {CashAmount}\n" +
               $"{nameof(Gender)}: {Gender}\n";
    }
}