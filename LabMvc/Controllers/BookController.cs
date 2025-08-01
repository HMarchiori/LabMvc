using Microsoft.AspNetCore.Mvc;

namespace LabMvc.Controllers;

public class BookController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}