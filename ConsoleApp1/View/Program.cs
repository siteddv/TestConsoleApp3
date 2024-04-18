using ConsoleApp1;
using ConsoleApp1.Controller;

string enterChoice = InputHelper
    .GetValueFromConsole($"Салам, братишка! Я тут вижу ты зашел в мой магазинчик, хочешь что-то купить? ({Choice.Accept}/{Choice.Decline})",
        Choice.Accept, Choice.Decline);
    
if(enterChoice.Equals(Choice.Accept, StringComparison.CurrentCultureIgnoreCase))
{
    Console.WriteLine("Отлично! Давай начнем!");
    Menu menu = new Menu();
    menu.ShowMenu();
}
else
{
    Console.WriteLine("Пидора ответ. Пошел нахуй!");
}