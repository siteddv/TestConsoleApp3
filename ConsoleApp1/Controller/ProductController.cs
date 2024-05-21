using System.Data.Common;
using Npgsql;

namespace ConsoleApp1.Controller;

public class ProductController
{
    public List<Product> GetAllTxt()
    {
        List<Product> allProducts = new List<Product>();

        string productsPath = "entities/products/";

        string[] filePaths = Directory.GetFiles(productsPath);
        
        foreach (string file in filePaths)
        {
            using StreamReader sr = new StreamReader(file);
            string articul = sr.ReadLine();
            string name = sr.ReadLine();
            string description = sr.ReadLine();
            decimal price = decimal.Parse(sr.ReadLine());
            
            Product Product = new Product(articul, name, description, price);
            allProducts.Add(Product);
        }
        
        return allProducts;
    }

    public void InsertTxt(List<Product> products)
    {
        string maxIdPath = "entities/products/max/max.txt";
        int maxId = 0;

        using (StreamReader sr = new StreamReader(maxIdPath))
        {
            maxId = int.Parse(sr.ReadLine());
        }
        
        foreach (Product product in products)
        {
            using (StreamWriter sw = new StreamWriter($"entities/products/product{maxId + 1}.txt"))
            {
                sw.WriteLine($"{product.Articul}");
                sw.WriteLine($"{product.Name}");
                sw.WriteLine($"{product.Description}");
                sw.WriteLine($"{product.Price}");
            }
            maxId++;
        }
        
        using (StreamWriter sw = new StreamWriter(maxIdPath))
        {
            sw.WriteLine($"{maxId}");
        }
    }
    
    public void InsertDb(List<Product> products)
    {
        using DbConnection connection = new NpgsqlConnection(PostgresController.ConnectionString);
        connection.Open();
        
        foreach (Product product in products)
        {
            string query = $"INSERT INTO product (article_number, name, description, price)" +
                           $"VALUES ('{product.Articul}', '{product.Name}', '{product.Description}', {product.Price})";
            using (DbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                command.ExecuteNonQuery();
            }
        }
    }

    public List<Product> GetAll()
    {
        List<Product> allProducts = new List<Product>();

        using DbConnection connection = new NpgsqlConnection(PostgresController.ConnectionString);
        connection.Open();
        
        string query = "SELECT id, article_number, name, description, price FROM product";
        using (DbCommand command = connection.CreateCommand())
        {
            command.CommandText = query;
            using (DbDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int idColumnNumber = reader.GetOrdinal("id");
                    long id = reader.GetInt64(idColumnNumber);
                    int articulColumnNumber = reader.GetOrdinal("article_number");
                    string articul = reader.GetString(articulColumnNumber);
                    int nameColumnNumber = reader.GetOrdinal("name");
                    string name = reader.GetString(nameColumnNumber);
                    int descriptionColumnNumber = reader.GetOrdinal("description");
                    string description = reader.GetString(descriptionColumnNumber);
                    int priceColumnNumber = reader.GetOrdinal("price");
                    decimal price = reader.GetDecimal(priceColumnNumber);
                    Product product = new Product(id, articul, name, description, price);
                    allProducts.Add(product);
                }
            }
        }
        
        return allProducts;
    }
}