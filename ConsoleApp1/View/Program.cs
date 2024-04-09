using ConsoleApp1;
using ConsoleApp1.Controller;

while (true)
{
    Console.WriteLine("enter name");
    string name = Console.ReadLine();
    Console.WriteLine("enter cash amount");
    decimal cashAmount = decimal.Parse(Console.ReadLine());
    Console.WriteLine("Enter gender");
    string gender = Console.ReadLine();
    Client client = new Client(name, cashAmount, gender);
    ClientController vasyaController = new ClientController(client);
    vasyaController.Save();
    Console.WriteLine("Client saved!");
}

