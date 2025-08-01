using LabMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace LabMvc.Controllers;

public class AuthorController : Controller
{
    private readonly AuthorService _authorService;

    public AuthorController(AuthorService authorService)
    {
        _authorService = authorService;
    }

    // Página inicial: form de busca
    public IActionResult Index()
    {
        return View();
    }

    // Busca por sobrenome
    [HttpGet]
    public async Task<IActionResult> Search(string lastName)
    {
        if (string.IsNullOrWhiteSpace(lastName))
        {
            TempData["Error"] = "Please enter a last name to search.";
            return RedirectToAction(nameof(Index));
        }

        var authors = await _authorService.FindByLastName(lastName);

        if (!authors.Any())
        {
            TempData["Error"] = "No authors found with that last name.";
            return RedirectToAction(nameof(Index));
        }

        return View("Results", authors);
    }
}