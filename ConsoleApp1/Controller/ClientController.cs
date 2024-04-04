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
        string[] filePaths = Directory.GetFiles("entities/clients/");
        
        string path = "entities/clients/client.txt";
        using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
        {
            using (StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default))
            {
                sw.WriteLine(_client.Name);
                sw.WriteLine(_client.CashAmount);
                sw.WriteLine(_client.Gender);
            }
        }
    }
}