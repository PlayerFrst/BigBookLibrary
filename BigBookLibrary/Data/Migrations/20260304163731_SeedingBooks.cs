using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BigBookLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingBooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AuthorId", "CopiesAvailable", "CoverImagePath", "Description", "GenreId", "ISBN", "IsDeleted", "Title", "Year" },
                values: new object[,]
                {
                    { 2, 1, 5, "/images/books/davinci.jpg", "A symbologist uncovers a conspiracy hidden in famous artworks.", 1, "9780307474278", false, "The Da Vinci Code", 2003 },
                    { 3, 1, 4, "/images/books/angels.jpg", "Robert Langdon investigates a murder linked to the Illuminati.", 1, "9780743493468", false, "Angels & Demons", 2000 },
                    { 4, 1, 6, "/images/books/inferno.jpg", "A mystery tied to Dante’s Inferno threatens global catastrophe.", 1, "9780804172264", false, "Inferno", 2013 },
                    { 5, 2, 10, "/images/books/hp1.jpg", "The beginning of Harry Potter’s magical journey.", 2, "9780747532699", false, "Harry Potter and the Philosopher's Stone", 1997 },
                    { 6, 2, 8, "/images/books/hp2.jpg", "Harry returns to Hogwarts to face a dark force.", 2, "9780747538493", false, "Harry Potter and the Chamber of Secrets", 1998 },
                    { 7, 2, 7, "/images/books/hp3.jpg", "A dangerous fugitive threatens Hogwarts.", 2, "9780747542155", false, "Harry Potter and the Prisoner of Azkaban", 1999 },
                    { 8, 3, 5, "/images/books/got1.jpg", "Noble families fight for control of the Iron Throne.", 3, "9780553103540", false, "A Game of Thrones", 1996 },
                    { 9, 3, 5, "/images/books/got2.jpg", "The Seven Kingdoms descend into war.", 3, "9780553108033", false, "A Clash of Kings", 1998 },
                    { 10, 3, 5, "/images/books/got3.jpg", "Betrayal and war reshape Westeros.", 3, "9780553106633", false, "A Storm of Swords", 2000 },
                    { 11, 1, 4, "/images/books/digital.jpg", "A cryptographer uncovers a threat to national security.", 1, "9780312944926", false, "Digital Fortress", 1998 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 11);
        }
    }
}
