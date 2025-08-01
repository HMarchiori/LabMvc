using LabMvc.Models;
using LabMvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace LabMvc.Controllers;

public class LibraryController : Controller
{
    private readonly LibraryService _service;
    private readonly BookService _bookService;

    public LibraryController(LibraryService service, BookService bookService)
    {
        _service = service;
        _bookService = bookService;
    }

    
    [HttpGet("")]
    public IActionResult Index()
    {
        return View();
    }
    
    [HttpGet("books/author")]
    public async Task<IActionResult> BooksByAuthor(int? authorId)
    {
        if (!authorId.HasValue || authorId <= 0)
        {
            return View(new List<(Book, bool, DateTime?)>()); // view vazia, só com search
        }

        var booksWithStatus = await _service.GetBooksByAuthor(authorId.Value);

        if (!booksWithStatus.Any())
        {
            TempData["Error"] = "No books found for this author ID.";
        }

        ViewBag.AuthorId = authorId.Value;
        return View(booksWithStatus);
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