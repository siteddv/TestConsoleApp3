using SlaveryMarket.BL;
using SlaveryMarket.BL.Controller;
using SlaveryMarket.Data;
using SlaveryMarket.Data.Dto;
using SlaveryMarket.Data.Model;
using SlaveryMarket.Data.Repository;

namespace SlaveryMarket.CMD;

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
                ShowBoughtProducts();
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

    private void ShowBoughtProducts()
    {
        ClientController clientController = new ClientController();
        var (products, counts) = clientController.GetBoughtProducts(DefaultConfiguration.DefaultUserId);
        for(int i=0; i<products.Count; i++)
        {
            Console.WriteLine(products[i]);
            Console.WriteLine($"Количество: {counts[i]}");
            Console.WriteLine();
        }
        Console.WriteLine("Нажмите любую клавишу для продолжения...");
        Console.ReadKey();
        ShowMenu();
    }

    private void BuyProduct()
    {
        ProductController productController = new ProductController();
        List<ProductDto> products = productController.GetAllDto();
        Console.WriteLine("Список продуктов");
        foreach (ProductDto product in products)
        {
            Console.WriteLine(product);
            Console.WriteLine();
        }

        var orderIntents = new List<OrderItemIntentDto>();
        
        var idCollection = products
            .Select(p => p.Id.ToString())
            .ToList();
        
        idCollection.Add("выход");
        
        while (true)
        {
            Console.WriteLine("Введите id продуктов через enter. чтобы выйти из покупки введите \"выход\"");
            
            string productIdToBuy = InputHelper.GetValueFromConsole("Введите нужный товар", idCollection.ToArray());
            if (productIdToBuy.Equals("выход"))
            {
                break;
            }
            var productId = long.Parse(productIdToBuy);
            Console.WriteLine("Введите количество товара");
            float amount = float.Parse(Console.ReadLine());
            
            var productPrice = products
                .First(p => p.Id == productId)
                .Price;
            var orderItemDto = new OrderItemIntentDto(productId, productPrice, amount);
            orderIntents.Add(orderItemDto);
        }

        // var orderIntentDto = new OrderIntentDto(DefaultConfiguration.DefaultUserId, orderIntents);
    }

    private void InsertProducts()
    {
        List<Product> products = GetProductsFromConsole();
        ProductController productController = new ProductController();
        productController.InsertMany(products);
        ShowMenu();
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