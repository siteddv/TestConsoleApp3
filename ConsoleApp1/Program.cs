using ConsoleApp1;

Product product = new Product();
product.Name = "Apple";
product.Price = 0.99m;
product.Articul = "123";
product.Description = "Red apple";

Product product2 = new Product();
product2.Name = "Banana";
product2.Price = 1.99m;
product2.Articul = "124";
product2.Description = "Yellow banana";

List<Product> products = new List<Product>();
products.Add(product);
products.Add(product2);

foreach (var p in products)
{
    Console.WriteLine("Name ");
    Console.WriteLine(p.Price);
    Console.WriteLine(p.Articul);
    Console.WriteLine(p.Description);
    Console.WriteLine();
}