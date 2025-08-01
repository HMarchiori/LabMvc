using LabMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace LabMvc.Controllers;

public class BookController : Controller
{
    private readonly BookService _bookService;

    public BookController(BookService bookService)
    {
        _bookService = bookService;
    }
    
    public async Task<IActionResult> Index()
    {
        var books = await _bookService.GetAll();
        return View(books);
    }
}