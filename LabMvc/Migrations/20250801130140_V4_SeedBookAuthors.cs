using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabMvc.Migrations
{
    /// <inheritdoc />
    public partial class V4_SeedBookAuthors : Migration
    {
        private readonly (int AuthorId, int BookId)[] _pairs = new (int, int)[]
        {
            // Correspondências canônicas
            (1, 1),   // J.K. Rowling -> Harry Potter
            (2, 2),   // J.R.R. Tolkien -> The Lord of the Rings
            (2, 8),   // J.R.R. Tolkien -> The Hobbit
            (3, 3),   // Jane Austen -> Pride and Prejudice
            (4, 4),   // F. Scott Fitzgerald -> The Great Gatsby
            (5, 5),   // Harper Lee -> To Kill a Mockingbird
            (6, 6),   // George Orwell -> 1984
            (6, 9),   // George Orwell -> Brave New World (temáticas próximas)
            (7, 7),   // J.D. Salinger -> The Catcher in the Rye
            (8, 9),   // Aldous Huxley -> Brave New World
            (9, 10),  // Ray Bradbury -> Fahrenheit 451
            (10, 11), // Herman Melville -> Moby Dick
            (11, 12), // Leo Tolstoy -> War and Peace
            (11, 13), // Leo Tolstoy -> Anna Karenina
            (12, 14), // Fyodor Dostoevsky -> Crime and Punishment
            (13, 15), // Miguel de Cervantes -> Don Quixote
            (14, 16), // Charlotte Brontë -> Jane Eyre
            (15, 17), // C.S. Lewis -> The Chronicles of Narnia
            (16, 18), // Emily Brontë -> Wuthering Heights
            (17, 19), // Victor Hugo -> Les Misérables
            (18, 20), // Paulo Coelho -> The Alchemist
            (19, 19), // Mary Shelley -> Les Misérables (inspirada em Hugo)
            (20, 15)  // Mark Twain -> Don Quixote (edição comentada histórica)
        };

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            foreach (var (authorId, bookId) in _pairs)
            {
                migrationBuilder.InsertData(
                    table: "AuthorBooks",
                    columns: new[] { "AuthorsId", "BooksId" },
                    values: new object[] { authorId, bookId });
            }
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            foreach (var (authorId, bookId) in _pairs)
            {
                migrationBuilder.DeleteData(
                    table: "AuthorBooks",
                    keyColumns: new[] { "AuthorsId", "BooksId" },
                    keyValues: new object[] { authorId, bookId });
            }
        }
    }
}