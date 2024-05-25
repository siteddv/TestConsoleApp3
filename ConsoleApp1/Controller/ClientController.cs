using System.Data;
using System.Data.Common;
using Npgsql;

namespace ConsoleApp1.Controller;

public class ClientController
{
    public void SaveTxt(Client client)
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

    public List<Client> GetAllTxt()
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

    public void BuyProduct(int productId, int amount, int defaultUserId)
    {
        using DbConnection connection = new NpgsqlConnection(PostgresController.ConnectionString);
        connection.Open();
        using DbTransaction transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);
        
        using DbCommand moneyWithdrawingCommand = connection.CreateCommand();
        moneyWithdrawingCommand.CommandText = 
            "UPDATE client " +
                " SET money_amount = money_amount - (SELECT price FROM product WHERE id = @product_id) * @amount " +
            "WHERE id = @client_id";
        moneyWithdrawingCommand.Parameters.Add(new NpgsqlParameter("@client_id", defaultUserId));
        moneyWithdrawingCommand.Parameters.Add(new NpgsqlParameter("@product_id", productId));
        moneyWithdrawingCommand.Parameters.Add(new NpgsqlParameter("@amount", amount));
        moneyWithdrawingCommand.ExecuteNonQuery();
        
        using DbCommand command = connection.CreateCommand();
        string query = "INSERT INTO client_products (client_id, product_id, amount) VALUES (@client_id, @product_id, @amount)";
        command.CommandText = query;
        command.Parameters.Add(new NpgsqlParameter("@client_id", defaultUserId));
        command.Parameters.Add(new NpgsqlParameter("@product_id", productId));
        command.Parameters.Add(new NpgsqlParameter("@amount", amount));
        command.ExecuteNonQuery();
        
        transaction.Commit();
    }
}