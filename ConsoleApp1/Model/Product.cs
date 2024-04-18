namespace ConsoleApp1;

public class Product
{
    public Product()
    {

    }
    public Product(string name, decimal price) 
    {
        Name = name;
        Price = price;
    }
    public Product(string articul, string name, string description, decimal price)
    {
        Name = name;
        Articul = articul;
        Description = description;
        Price = price;
    }

    public string Name;
    public string Articul;
    public string Description;
    public decimal Price;

    public override string ToString()
    {
        return "Name: " + Name + "\n" +
               "Articul: " + Articul + "\n" +
               "Description: " + Description + "\n" +
               "Price: " + Price;
    }
}

