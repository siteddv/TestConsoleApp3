namespace ConsoleApp1.Controller;

public static class InputHelper
{
    public static string GetValueFromConsole(string message, params string[] possibleValues)
    {
        Console.WriteLine(message);
        string value = Console.ReadLine();
        if (possibleValues.Length == 0)
        {
            return value;
        }
        while (!possibleValues.Contains(value, StringComparer.OrdinalIgnoreCase))
        {
            Console.WriteLine("Ты неправильно ввел значение, попробуй еще раз!");
            var possibleValuesString = string.Join(", ", possibleValues);
            Console.WriteLine($"Возможные значения: {possibleValuesString}");
            value = Console.ReadLine();
        }
        return value;
    }
}