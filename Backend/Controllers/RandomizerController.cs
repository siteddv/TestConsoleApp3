using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

public class RandomizerController : Controller
{
    public ActionResult RandomNumber(int? start, int? end)
    {
        var random = new Random();
        int randomNumber = random.Next(start.Value, end.Value + 1);
        ViewBag.RandomNumber = randomNumber;
        return View();
    }
}