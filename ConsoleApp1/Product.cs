namespace ConsoleApp1;

public class Product
{
    static Product()
    {
        Console.WriteLine("STAIC COSNTRUCTOR");
    }

    //вызывается 1 раз при обращении к класс
    //нельзя сделать перегрузку
    //нет входных параметров
    //можно использовать в любых классах
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
}

