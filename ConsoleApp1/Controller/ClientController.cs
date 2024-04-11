namespace ConsoleApp1.Controller;

public class ClientController
{
    public void Save(Client client)
    {
        string maxIdPath = "entities/clients/max/max.txt";
        int maxId = 0;

        using (StreamReader sr = new StreamReader(maxIdPath))
        {
            maxId = int.Parse(sr.ReadLine());
        }
        
        using (StreamWriter sw = new StreamWriter($"entities/clients/client{maxId + 1}.txt"))
        {
            sw.WriteLine($"{client.Name}");
            sw.WriteLine($"{client.CashAmount}");
            sw.WriteLine($"{client.Gender}");
        }
        
        using (StreamWriter sw = new StreamWriter(maxIdPath))
        {
            sw.WriteLine($"{maxId + 1}");
        }
    }

    public List<Client> GetAll()
    {
        List<Client> allClients = new List<Client>();

        string clientsPath = "entities/clients/";

        string[] filePaths = Directory.GetFiles(clientsPath);
        
        foreach (string file in filePaths)
        {
            using StreamReader sr = new StreamReader(file);
            string name = sr.ReadLine();
            decimal cashAmount = decimal.Parse(sr.ReadLine());
            string gender = sr.ReadLine();
            Client client = new Client(name, cashAmount, gender);
            allClients.Add(client);
        }
        
        return allClients;
    }
}