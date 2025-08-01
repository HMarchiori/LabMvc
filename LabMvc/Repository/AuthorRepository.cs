using Microsoft.EntityFrameworkCore;
using LabMvc.Data;
using LabMvc.Models;
using LabMvc.Repository.Interfaces;

namespace LabMvc.Repository;

public class AuthorRepository(Context context) : IRepository<Author>
{
    public async Task<List<Author>> GetAll()
    {
        return await context.Authors
            .Include(a => a.Books)
            .ToListAsync();
    }

    public async Task<Author> FindById(int id)
    {
        return await context.Authors
                   .Include(a => a.Books)
                   .FirstOrDefaultAsync(a => a.Id == id)
               ?? throw new KeyNotFoundException($"Author with ID {id} not found.");
    }

    public async Task<List<Author>> FindByLastName(string lastName)
    {
        return await context.Authors
            .Include(a => a.Books)
            .Where(a => a.LastName.Contains(lastName))
            .ToListAsync();
    }

    public async Task Create(Author author)
    {
        if (author == null)
            throw new ArgumentNullException(nameof(author), "Author cannot be null.");

        await context.Authors.AddAsync(author);
        await context.SaveChangesAsync();
    }

    public async Task Update(Author author)
    {
        var existing = await context.Authors.FindAsync(author.Id);

        if (existing == null)
            throw new KeyNotFoundException($"Author with ID {author.Id} not found.");

        context.Entry(existing).CurrentValues.SetValues(author);
        await context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var author = await context.Authors.FindAsync(id);

        if (author == null)
            throw new KeyNotFoundException($"Author with ID {id} not found.");

        context.Authors.Remove(author);
        await context.SaveChangesAsync();
    }
}