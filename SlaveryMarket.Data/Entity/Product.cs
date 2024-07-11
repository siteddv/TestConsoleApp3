using SlaveryMarket.Data.Dto;

namespace SlaveryMarket.Data.Model;

public class Product
{
    public Product()
    {

    }
    
    public Product(string articul, string name, string description, decimal price)
    {
        Name = name;
        Articul = articul;
        Description = description;
        Price = price;
    }
    
    public Product(long id, string articul, string name, string description, decimal price) 
        : this(articul, name, description, price)
    {
        Id = id;
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public string Articul { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    
    public ProductDto ToDto()
    {
        return new ProductDto(Id, Name, Price);
    }

    public override string ToString()
    {
        return "Id: " + Id + "\n" +
               "Name: " + Name + "\n" +
               "Articul: " + Articul + "\n" +
               "Description: " + Description + "\n" +
               "Price: " + Price;
    }
}

