using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabMvc.Migrations
{
    /// <inheritdoc />
    public partial class V2_SeedBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Harry Potter and the Sorcerer's Stone" },
                    { 2, "The Lord of the Rings" },
                    { 3, "Pride and Prejudice" },
                    { 4, "The Great Gatsby" },
                    { 5, "To Kill a Mockingbird" },
                    { 6, "1984" },
                    { 7, "The Catcher in the Rye" },
                    { 8, "The Hobbit" },
                    { 9, "Brave New World" },
                    { 10, "Fahrenheit 451" },
                    { 11, "Moby Dick" },
                    { 12, "War and Peace" },
                    { 13, "Anna Karenina" },
                    { 14, "Crime and Punishment" },
                    { 15, "Don Quixote" },
                    { 16, "Jane Eyre" },
                    { 17, "The Chronicles of Narnia" },
                    { 18, "Wuthering Heights" },
                    { 19, "Les Misérables" },
                    { 20, "The Alchemist" }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Remover os 20 livros se rollback
            for (int i = 1;  i <= 20; i++)
            {
                migrationBuilder.DeleteData(
                    table: "Books",
                    keyColumn: "Id",
                    keyValue: i
                );
            }
        }
    }
}