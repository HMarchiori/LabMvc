using LabMvc.Models;
using LabMvc.Repository;

namespace LabMvc.Services;

public class LibraryService
{
    private readonly BookRepository _bookRepository;
    private readonly LoanRepository _loanRepository;

    public LibraryService(BookRepository bookRepository, LoanRepository loanRepository)
    {
        _bookRepository = bookRepository;
        _loanRepository = loanRepository;
    }

  
    public async Task<List<(Book Book, bool IsAvailable, DateTime? DueDate)>> GetBooksByAuthor(int authorId)
    {
        // Busca todos os livros do autor
        var books = await _bookRepository.GetByAuthorId(authorId);

        var bookAvailability = new List<(Book, bool, DateTime?)>();

        foreach (var book in books)
        {
            var activeLoan = await _loanRepository.FindLoanByBookId(book.Id);
            bookAvailability.Add((book, activeLoan is not { IsDelivered: true } , activeLoan?.DevolutionDate));
        }

        return bookAvailability;
    }

    public async Task BorrowBook(int bookId)
    {
        var activeLoan = await _loanRepository.FindLoanByBookId(bookId);
        if (activeLoan != null)
            throw new InvalidOperationException($"Book {bookId} is already borrowed.");

        var loan = new Loan
        {
            DepartDate = DateTime.Now,
            DevolutionDate = DateTime.Now.AddDays(7),
            IsDelivered = false,
            BookId = bookId
        };

        await _loanRepository.Create(loan);
    }
    
    
    public async Task<decimal> ReturnBook(int bookId)
    {
        var loan = await _loanRepository.FindLoanByBookId(bookId);

        if (loan == null)
            throw new KeyNotFoundException("The book is not currently borrowed.");

        loan.IsDelivered = true;
        await _loanRepository.Update(loan);

        var lateDays = (DateTime.Now - loan.DevolutionDate).Days;
        return lateDays > 0 ? lateDays * 2.00m : 0.00m; 
    }
}