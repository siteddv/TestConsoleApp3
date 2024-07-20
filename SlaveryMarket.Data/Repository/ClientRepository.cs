using System.Data.Common;
using Npgsql;
using SlaveryMarket.Data.Entity;
using SlaveryMarket.Data.Model;

namespace SlaveryMarket.Data.Repository;

public class ClientRepository
{
    public List<Client> GetAll()
    {
        using DbConnection connection = new NpgsqlConnection(DefaultConfiguration.ConnectionString);
        connection.Open();
        
        string query = """
                      SELECT 
                          name, 
                          money_amount,
                          phone
                      FROM 
                          client
                      """;
        using DbCommand command = connection.CreateCommand();
        command.CommandText = query;
        using DbDataReader reader = command.ExecuteReader();
        List<Client> clients = new List<Client>();
        while (reader.Read())
        {
            string name = reader.GetString(reader.GetOrdinal("name"));
            decimal moneyAmount = reader.GetDecimal(reader.GetOrdinal("money_amount"));
            string phone = reader.GetString(reader.GetOrdinal("phone"));
            clients.Add(new Client(name, moneyAmount, phone));
        }

        return clients;
    }
}