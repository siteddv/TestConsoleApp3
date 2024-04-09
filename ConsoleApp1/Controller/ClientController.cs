namespace ConsoleApp1.Controller;

public class ClientController
{
    private readonly Client _client;

    public ClientController(Client client)
    {
        _client = client;
    }

    public void BuyProduct(Product product)
    {
        _client.BoughtProducts.Add(product);
        _client.CashAmount -= product.Price;
        Console.WriteLine("You bought a product!");
    }

    public void Save()
    {
        string maxIdPath = "entities/clients/max.txt";
        int maxId = 0;

        using (StreamReader sr = new StreamReader(maxIdPath))
        {
            maxId = int.Parse(sr.ReadLine());
        }
        
        using (StreamWriter sw = new StreamWriter($"entities/clients/client{maxId + 1}.txt"))
        {
            sw.WriteLine($"{_client.Name}");
            sw.WriteLine($"{_client.CashAmount}");
            sw.WriteLine($"{_client.Gender}");
        }
        
        using (StreamWriter sw = new StreamWriter(maxIdPath))
        {
            sw.WriteLine($"{maxId + 1}");
        }
    }
}