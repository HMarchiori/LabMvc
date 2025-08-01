using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabMvc.Migrations
{
    /// <inheritdoc />
    public partial class V3_SeedAuthors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "J.K.", "Rowling" },
                    { 2, "J.R.R.", "Tolkien" },
                    { 3, "Jane", "Austen" },
                    { 4, "F. Scott", "Fitzgerald" },
                    { 5, "Harper", "Lee" },
                    { 6, "George", "Orwell" },
                    { 7, "J.D.", "Salinger" },
                    { 8, "Aldous", "Huxley" },
                    { 9, "Ray", "Bradbury" },
                    { 10, "Herman", "Melville" },
                    { 11, "Leo", "Tolstoy" },
                    { 12, "Fyodor", "Dostoevsky" },
                    { 13, "Miguel", "de Cervantes" },
                    { 14, "Charlotte", "Brontë" },
                    { 15, "C.S.", "Lewis" },
                    { 16, "Emily", "Brontë" },
                    { 17, "Victor", "Hugo" },
                    { 18, "Paulo", "Coelho" },
                    { 19, "Mary", "Shelley" },
                    { 20, "Mark", "Twain" }
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            for (int i = 1; i <= 20; i++)
            {
                migrationBuilder.DeleteData(
                    table: "Authors",
                    keyColumn: "Id",
                    keyValue: i
                );
            }
        }
    }
}