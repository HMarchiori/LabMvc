using LabMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace LabMvc.Controllers;

public class LibraryController : Controller
{
    private readonly LibraryService _service;

    public LibraryController(LibraryService service)
    {
        _service = service;
    }

    
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet("books/author/{authorId:int}")]
    public async Task<IActionResult> BooksByAuthor(int authorId)
    {
        var books = await _service.GetBooksByAuthor(authorId);
        ViewBag.AuthorId = authorId;
        return View(books); 
    }


    [HttpPost("borrow")]
    public async Task<IActionResult> Borrow(int bookId, int authorId)
    {
        try
        {
            await _service.BorrowBook(bookId);
            TempData["Success"] = "Book borrowed successfully!";
        }
        catch (InvalidOperationException ex)
        {
            TempData["Error"] = ex.Message;
        }
        return RedirectToAction(nameof(BooksByAuthor), new { authorId });
    }


    [HttpPost("return")]
    public async Task<IActionResult> Return(int bookId, int authorId)
    {
        try
        {
            var fine = await _service.ReturnBook(bookId);
            TempData["Success"] = fine > 0 
                ? $"Book returned! Late fee: ${fine:F2}" 
                : "Book returned on time. No fee charged.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }
        return RedirectToAction(nameof(BooksByAuthor), new { authorId });
    }
}