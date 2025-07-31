using LabMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace LabMvc.Data;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Loan> Loans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Author>()
            .HasMany(a => a.Books)
            .WithMany(b => b.Authors)
            .UsingEntity(j => j.ToTable("AuthorBooks"));

        modelBuilder.Entity<Book>()
            .HasMany(b => b.Loans)
            .WithOne(l => l.Book)
            .HasForeignKey(l => l.BookId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}