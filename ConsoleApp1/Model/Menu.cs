using ConsoleApp1.Controller;

namespace ConsoleApp1;

public class Menu
{
    public void ShowMenu()
    {
        Console.Clear();
        Console.WriteLine("Меню:");
        Console.WriteLine("1. Показать товары");
        Console.WriteLine("2. Показать купленные товары");
        Console.WriteLine("3. Выйти из магазина");
        
        string choice = Console.ReadLine();
        switch (choice)
        {
            case "1":
                ShowProducts();
                break;
            case "2":
                break;
            case "3":
                Exit();
                break;
            default:
                HandleIncorrectPoint();
                break;
        }
    }

    private void ShowProducts()
    {
        Console.Clear();
        ProductController productController = new ProductController();
        List<Product> products = productController.GetAll();
        foreach (Product product in products)
        {
            Console.WriteLine(product);
            Console.WriteLine();
        }

        List<string> possibleChoices = products
            .Select(p => p.Articul)
            .ToList();
        possibleChoices.Add("0");
        possibleChoices.Sort();
        
        string choice =
            InputHelper.GetValueFromConsole(
                "Введите артикул товара, который хотите купить или введите 0, чтобы вернуться в меню", possibleChoices.ToArray());

        Console.ReadKey();
    }

    private void HandleIncorrectPoint()
    {
        ShowMessage("Ты ввел неправильное значение, попробуй еще раз!");
        ShowMenu();
    }
    
    private void Exit()
    {
        ShowMessage("Спасибо за покупку! Пошел нахуй!");
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