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
        var books = (await _bookRepository.GetAll())
            .Where(b => b.Authors.Any(a => a.Id == authorId))
            .ToList();

        var result = new List<(Book, bool, DateTime?)>();

        foreach (var book in books)
        {
            var activeLoan = await _loanRepository.FindLoanByBookId(book.Id);
            result.Add((book, false, activeLoan.DevolutionDate));
        }
        return result;
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