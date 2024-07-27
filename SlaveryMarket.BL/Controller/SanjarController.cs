using SlaveryMarket.Data.Model;

namespace SlaveryMarket.BL.Controller;

public class SanjarController
{
    public void InsertTo(object obj, string name = "Azamat")
    {
        Janar Janar = new Janar(name);
        obj = Janar;
        WakeUpRustam(obj.ToString());
    }

    public void WakeUpRustam(dynamic obj)
    {
        Console.WriteLine("Rustam Wake Up!{0}", obj);
        Console.Beep();
    }
}