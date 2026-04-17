using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BigBookLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedBorrowings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Borrowings",
                columns: new[] { "Id", "BookId", "BorrowedOn", "DueDate", "ReturnedOn", "UserId" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2026, 3, 28, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2206), new DateTime(2026, 4, 11, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2212), null, "2527ff55-6195-49b4-8f13-4f420775862d" },
                    { 2, 3, new DateTime(2026, 4, 14, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2215), new DateTime(2026, 4, 28, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2216), null, "22f27b10-8477-4db4-a0f1-024e9e7e369d" },
                    { 3, 4, new DateTime(2026, 4, 12, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2217), new DateTime(2026, 4, 26, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2218), null, "22f27b10-8477-4db4-a0f1-024e9e7e369d" },
                    { 4, 5, new DateTime(2026, 4, 5, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2219), new DateTime(2026, 4, 19, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2219), new DateTime(2026, 4, 15, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2220), "22f27b10-8477-4db4-a0f1-024e9e7e369d" },
                    { 5, 6, new DateTime(2026, 4, 2, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2223), new DateTime(2026, 4, 16, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2223), new DateTime(2026, 4, 14, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2224), "22f27b10-8477-4db4-a0f1-024e9e7e369d" },
                    { 6, 7, new DateTime(2026, 4, 16, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2225), new DateTime(2026, 4, 30, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2225), null, "e21751ac-1fa9-41f8-9026-acf50ca8d9fb" },
                    { 7, 8, new DateTime(2026, 4, 13, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2226), new DateTime(2026, 4, 27, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2227), null, "e21751ac-1fa9-41f8-9026-acf50ca8d9fb" },
                    { 8, 9, new DateTime(2026, 3, 30, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2228), new DateTime(2026, 4, 13, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2229), new DateTime(2026, 4, 12, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2229), "e21751ac-1fa9-41f8-9026-acf50ca8d9fb" },
                    { 9, 10, new DateTime(2026, 4, 10, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2231), new DateTime(2026, 4, 24, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2231), new DateTime(2026, 4, 16, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2232), "e21751ac-1fa9-41f8-9026-acf50ca8d9fb" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
