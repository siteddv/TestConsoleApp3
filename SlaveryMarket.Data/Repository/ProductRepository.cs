using System.Data.Common;
using System.Text;
using Npgsql;
using SlaveryMarket.Data.Model;

namespace SlaveryMarket.Data.Repository;

public class ProductRepository
{
    public int Sanjar
    {
        get
        {
            _sanjar++;
            return _sanjar;
        }
        set
        {
            _sanjar = value;
        }
    }

    private int _sanjar = 9;
    
    public void InsertMany(List<Product> products)
    {
        using DbConnection connection = new NpgsqlConnection(DefaultConfiguration.ConnectionString);
        connection.Open();
        string productsForBulkInsert = GetProductsForBulkInsert(products);
        string query = $"INSERT INTO product (article_number, name, description, price) " +
                       $"VALUES {productsForBulkInsert};";
        using DbCommand command = connection.CreateCommand();
        command.CommandText = query;
        command.ExecuteNonQuery();
    }

    private string GetProductsForBulkInsert(List<Product> products)
    {
        StringBuilder sb = new StringBuilder();

        foreach (var product in products)
        {
            sb.Append($"('{product.Articul}', '{product.Name}', '{product.Description}', {product.Price}),");
            // ('123', 'Janar', 'young and beautiful', 10),
        }

        sb.Remove(sb.Length - 1, 1);

        return sb.ToString();
    }

    public List<Product> GetAll()
    {
        List<Product> allProducts = new List<Product>();
        
        using DbConnection connection = new NpgsqlConnection(DefaultConfiguration.ConnectionString);
        connection.Open();
        
        string query = """
                      SELECT 
                          id, 
                          article_number, 
                          name, 
                          description, 
                          price 
                      FROM 
                          product
                      """;
        using DbCommand command = connection.CreateCommand();
        command.CommandText = query;
        using DbDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            long id = reader.GetInt64(reader.GetOrdinal("id"));
            string articul = reader.GetString(reader.GetOrdinal("article_number"));
            string name = reader.GetString(reader.GetOrdinal("name"));
            string description = reader.GetString(reader.GetOrdinal("description"));
            decimal price = reader.GetDecimal(reader.GetOrdinal("price"));
            Product product = new Product(id, articul, name, description, price);
            allProducts.Add(product);
        }

        return allProducts;
    }
}