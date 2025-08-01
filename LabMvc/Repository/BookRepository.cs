using Microsoft.EntityFrameworkCore;
using LabMvc.Data;
using LabMvc.Models;
using LabMvc.Repository.Interfaces;

namespace LabMvc.Repository;

public class BookRepository(Context context) : IRepository<Book>
{
    public async Task<List<Book>> GetAll()
    {
        return await context.Books
            .Include(b => b.Authors)
            .Include(b => b.Loans)
            .ToListAsync();
    }

    public async Task<Book> FindById(int id)
    {
        return await context.Books
                   .Include(b => b.Authors)
                   .Include(b => b.Loans)
                   .FirstOrDefaultAsync(b => b.Id == id)
               ?? throw new KeyNotFoundException($"Book with ID {id} not found.");
    }

    public async Task Create(Book book)
    {
        if (book == null)
            throw new ArgumentNullException(nameof(book), "Book cannot be null.");

        await context.Books.AddAsync(book);
        await context.SaveChangesAsync();
    }

    public async Task Update(Book book)
    {
        var existing = await context.Books.FindAsync(book.Id);

        if (existing == null)
            throw new KeyNotFoundException($"Book with ID {book.Id} not found.");

        context.Entry(existing).CurrentValues.SetValues(book);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var book = await context.Books.FindAsync(id);

        if (book == null)
            throw new KeyNotFoundException($"Book with ID {id} not found.");

        context.Books.Remove(book);
        await context.SaveChangesAsync();
    }

    public async Task<List<Book>> GetByAuthorId(int authorId)
    {
        return await context.Books
            .Include(b => b.Authors)
            .Include(b => b.Loans)
            .Where(b => b.Authors.Any(a => a.Id == authorId))
            .ToListAsync();
    }
}