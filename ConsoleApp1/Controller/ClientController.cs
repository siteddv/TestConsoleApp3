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

    public (List<Product>, List<int>) GetBoughtProducts(int defaultUserId)
    {
        List<Product> products = new List<Product>();
        List<int> counts = new List<int>();
        using DbConnection connection = new NpgsqlConnection(PostgresController.ConnectionString);
        connection.Open();
        string query = "SELECT p.id, p.article_number, p.name, p.description, p.price, SUM(cp.amount) as amount " +
                       "FROM product p " +
                       "left JOIN client_products cp ON p.id = cp.product_id " +
                       "WHERE cp.client_id = @client_id " +
                       "group by p.article_number, p.id, p.article_number, p.name, p.description, p.price";
        using DbCommand command = connection.CreateCommand();
        command.CommandText = query;
        command.Parameters.Add(new NpgsqlParameter("@client_id", defaultUserId));
        using DbDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            long id = reader.GetInt64(reader.GetOrdinal("id"));
            string articul = reader.GetString(reader.GetOrdinal("article_number"));
            string name = reader.GetString(reader.GetOrdinal("name"));
            string description = reader.GetString(reader.GetOrdinal("description"));
            decimal price = reader.GetDecimal(reader.GetOrdinal("price"));
            int amount = reader.GetInt32(reader.GetOrdinal("amount"));
            counts.Add(amount);
            Product product = new Product(id, articul, name, description, price);
            products.Add(product);
        }

        return (products, counts);
    }
}