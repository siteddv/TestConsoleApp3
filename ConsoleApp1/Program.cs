using ConsoleApp1;

List<Product> products = new List<Product>();
Product product1 = new Product
{
    Name = "Product1", 
    Price = 100, 
    Articul = "Articul1", 
    Description = "Description1"
};
products.Add(product1);
products.Add(new Product 
    { 
        Name = "Product2", 
        Price = 200, 
        Articul = "Articul2", 
        Description = "Description2" 
    }
);
products.Add(new Product { Name = "Product3", Price = 300, Articul = "Articul3", Description = "Description3" });
PrintProductsInfo(products);

Client client = new Client("Client1", 1000, "Doublesexual");
Console.WriteLine("What products do you want to buy?");
int productIndex = int.Parse(Console.ReadLine());
Product selectedProduct = products[productIndex - 1];
client.BuyProduct(selectedProduct);

// int productCount = GetProductCountFromConsole();
// List<Product> products = GetProductsInfo(productCount);
// PrintProductsInfo(products);
//
static void PrintProductsInfo(List<Product> products)
{
    Console.WriteLine("Info about products:");
    int i = 1;
    foreach (var p in products)
    {
        Console.WriteLine($"#{i}");
        PrintProduct(p);
        i++;
    }
}
//
// static List<Product> GetProductsInfo(int productCount)
// {
//     List<Product> products = new List<Product>();
//     for (int i = 1; i <= productCount; i++)
//     {
//         Console.WriteLine($"Enter info about product {i}");
//         Product product = GetProductFromConsole();
//         products.Add(product);
//     }
//
//     return products;
// }
//
// static int GetProductCountFromConsole()
// {
//     Console.WriteLine("Enter number of products that you want to add");
//     return int.Parse(Console.ReadLine());
// }
//
static void PrintProduct(Product product)
{
    Console.WriteLine(product.Name);
    Console.WriteLine(product.Price);
    Console.WriteLine(product.Articul);
    Console.WriteLine(product.Description);
    Console.WriteLine();
}
//
// static Product GetProductFromConsole()
// {
//     Product product = new Product();
//
//     Console.Write("Enter product name: ");
//     product.Name = Console.ReadLine();
//
//     Console.Write("Enter product price: ");
//     product.Price = decimal.Parse(Console.ReadLine());
//
//     Console.Write("Enter product articul: ");
//     product.Articul = Console.ReadLine();
//
//     Console.Write("Enter product description: ");
//     product.Description = Console.ReadLine();
//
//     return product;
// }