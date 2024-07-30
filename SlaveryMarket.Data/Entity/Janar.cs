using SlaveryMarket.Data.Entity;

namespace SlaveryMarket.Data.Model;

public class Janar : ApplicationUser
{
    public Janar(string gender) 
    {
        Gender = gender;
    }
    public string Gender {  get; set; }
}