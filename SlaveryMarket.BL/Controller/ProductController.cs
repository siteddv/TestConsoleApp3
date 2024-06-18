using System.Data.Common;
using SlaveryMarket.Data.Dto;
using SlaveryMarket.Data.Model;
using SlaveryMarket.Data.Repository;

namespace SlaveryMarket.BL.Controller;

public class ProductController
{
    private readonly ProductRepository _productRepository;
    public ProductController()
    {
        _productRepository = new ProductRepository();
    }
    
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
        
    }

    public void InsertMany(List<Product> products)
    {
        _productRepository.InsertMany(products);
    }

    public List<Product> GetAll()
    {
        return _productRepository.GetAll();
    }
    
    public List<ProductDto> GetAllDto()
    {
        return _productRepository
            .GetAll()
            .Select(product => product.ToDto())
            .ToList();
    } 
}