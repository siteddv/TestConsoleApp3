namespace ConsoleApp1.Controller;

public class ProductController
{
    public List<Product> GetAll()
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
}