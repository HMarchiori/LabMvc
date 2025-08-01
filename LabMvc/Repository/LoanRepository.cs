using Microsoft.EntityFrameworkCore;
using LabMvc.Data;
using LabMvc.Models;
using LabMvc.Repository.Interfaces;

namespace LabMvc.Repository;

public class LoanRepository(Context context) : IRepository<Loan>
{
    public async Task<List<Loan>> GetAll()
    {
        return await context.Loans
            .Include(l => l.Book)
            .ToListAsync();
    }

    public async Task<Loan> FindById(int id)
    {
        return await context.Loans
                   .Include(l => l.Book)
                   .FirstOrDefaultAsync(l => l.Id == id)
               ?? throw new KeyNotFoundException($"Loan with ID {id} not found.");
    }

    public async Task<Loan> FindLoanByBookId(int bookId)
    {
        return await context.Loans
                   .Include(l => l.Book)
                   .FirstOrDefaultAsync(l => l.BookId == bookId && l.IsDelivered) 
               ?? throw new KeyNotFoundException($"Loan with Book ID {bookId} not found.");
    }

    public async Task Create(Loan loan)
    {
        if (loan == null)
            throw new ArgumentNullException(nameof(loan), "Loan cannot be null.");

        await context.Loans.AddAsync(loan);
        await context.SaveChangesAsync();
    }

    public async Task Update(Loan loan)
    {
        var existing = await context.Loans.FindAsync(loan.Id);

        if (existing == null)
            throw new KeyNotFoundException($"Loan with ID {loan.Id} not found.");

        context.Entry(existing).CurrentValues.SetValues(loan);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var loan = await context.Loans.FindAsync(id);

        if (loan == null)
            throw new KeyNotFoundException($"Loan with ID {id} not found.");

        context.Loans.Remove(loan);
        await context.SaveChangesAsync();
    }
}