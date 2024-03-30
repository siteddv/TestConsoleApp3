using ConsoleApp1;

int productCount = GetProductCountFromConsole();
List<Product> products = GetProductsInfo(productCount);
PrintProductsInfo(products);

Product product = new Product("Bread", 100);
Product product2 = new Product
    (
    "Bread","121 222 232","Vkusniy hleb, чтоб не втыкали", 100, 10
    );
products.Add(product);
products.Add(product2 );
PrintProductsInfo (products);


static void PrintProductsInfo(List<Product> products)
{
    Console.WriteLine("Info about products:");
    foreach (var p in products)
    {
        PrintProduct(p);
    }
}

static List<Product> GetProductsInfo(int productCount)
{
    List<Product> products = new List<Product>();
    for (int i = 0; i < productCount; i++)
    {
        Console.WriteLine($"Enter info about product {i + 1}");
        Product product = GetProductFromConsole();
        products.Add(product);
    }

    return products;
}

static int GetProductCountFromConsole()
{
    Console.WriteLine("Enter number of products that you want to add");
    return int.Parse(Console.ReadLine());
}

static void PrintProduct(Product product)
{
    Console.WriteLine(product.Name);
    Console.WriteLine(product.Price);
    Console.WriteLine(product.Articul);
    Console.WriteLine(product.Description);
    Console.WriteLine();
}

static Product GetProductFromConsole()
{
    Product product = new Product();

    Console.Write("Enter product name: ");
    product.Name = Console.ReadLine();

    Console.Write("Enter product price: ");
    product.Price = decimal.Parse(Console.ReadLine());

    Console.Write("Enter product articul: ");
    product.Articul = Console.ReadLine();

    Console.Write("Enter product description: ");
    product.Description = Console.ReadLine();

    return product;
}