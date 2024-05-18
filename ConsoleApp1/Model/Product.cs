namespace ConsoleApp1;

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
    public string Name;
    public string Articul;
    public string Description;
    public decimal Price;

    public override string ToString()
    {
        return "Id: " + Id + "\n" +
               "Name: " + Name + "\n" +
               "Articul: " + Articul + "\n" +
               "Description: " + Description + "\n" +
               "Price: " + Price;
    }
}

