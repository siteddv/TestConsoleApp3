using ConsoleApp1.Controller;

string enterChoice = InputHelper
    .GetValueFromConsole($"Салам, братишка! Я тут вижу ты зашел в мой магазинчик, хочешь что-то купить? ({Choice.Accept}/{Choice.Decline})",
        Choice.Accept, Choice.Decline);
    
if(enterChoice.Equals(Choice.Accept, StringComparison.CurrentCultureIgnoreCase))
{
    Console.WriteLine("Отлично! Давай начнем!");
}
else
{
    Console.WriteLine("Ну и ладно, заходи еще!");
}