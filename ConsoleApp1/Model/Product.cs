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
    public Product(string name, string articul, string description, decimal price, decimal markUp)
    {
        Name = name;
        Articul = articul;
        Description = description;
        Price = price;
        OptPrice = OptPrice + markUp;
    }

    public string Name;
    public string Articul;
    public string Description;

    public decimal Price;
    private decimal OptPrice = 25;

    public override string ToString()
    {
        return "Name: " + Name + "\n" +
               "Articul: " + Articul + "\n" +
               "Description: " + Description + "\n" +
               "Price: " + Price + "\n" +
               "OptPrice: " + OptPrice + "\n";
    }
}

