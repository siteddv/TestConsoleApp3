using ConsoleApp1.Controller;
using ConsoleApp1.Helper;

namespace ConsoleApp1;

public class Menu
{
    public void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("Меню:");
        Console.WriteLine("1. Показать товары");
        Console.WriteLine("2. Показать купленные товары");
        Console.WriteLine("3. Добавить товары");
        Console.WriteLine("4. Купить товары");
        Console.WriteLine("5. Выйти из магазина");

        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                ShowProducts();
                break;
            case "2":
                break;
            case "3":
                InsertProducts();
                break;
            case "4":
                BuyProduct();
                break;
            case "5":
                Exit();
                break;
            default:
                HandleIncorrectPoint();
                break;
        }
    }

    private void BuyProduct()
    {
        PrintProducts();
        Console.WriteLine("Введите id товара, который хотите купить");
        int productId = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите количество товара");
        int count = int.Parse(Console.ReadLine());
        Console.WriteLine("Введите id клиента");
        Console.WriteLine(1);
        ClientController clientController = new ClientController();
        clientController.BuyProduct(productId, count, DefaultConfiguration.DefaultUserId);
        Console.WriteLine("Товар успешно куплен!");
    }

    private void InsertProducts()
    {
        List<Product> products = GetProductsFromConsole();
        ProductController productController = new ProductController();
        productController.InsertDb(products);
    }

    private List<Product> GetProductsFromConsole()
    {
        List<Product> products = new List<Product>();
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Введите артикул товара");
            string articul = Console.ReadLine();
            Console.WriteLine("Введите название товара");
            string name = Console.ReadLine();
            Console.WriteLine("Введите описание товара");
            string description = Console.ReadLine();
            Console.WriteLine("Введите цену товара");
            decimal price = decimal.Parse(Console.ReadLine());
            Product product = new Product(articul, name, description, price);
            products.Add(product);
            Console.WriteLine("Добавить еще один товар? (да/нет)");
            string choice = Console.ReadLine();
            if (choice.Equals("нет"))
            {
                break;
            }
        }

        return products;
    }

    private void GetDbInfo()
    {
        PostgresController postgresController = new PostgresController();
        postgresController.GetDbInfo();
    }

    private void ShowProducts()
    {
        PrintProducts();
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
        ShowMenu();
    }
    
    private void PrintProducts()
    {
        ProductController productController = new ProductController();
        List<Product> products = productController.GetAll();
        foreach (Product product in products)
        {
            Console.WriteLine(product);
            Console.WriteLine();
        }
    }

    private void HandleIncorrectPoint()
    {
        ShowMessage("Ты ввел неправильное значение, попробуй еще раз!");
        ShowMenu();
    }
    
    private void Exit()
    {
        ShowMessage("Спасибо за покупку! всего хорошего.");
        Environment.Exit(0);
    }

    private void ShowMessage(string msg)
    {
        Console.Clear();
        Console.WriteLine(msg);
        Thread.Sleep(2000);
        Console.Clear();
    }
}