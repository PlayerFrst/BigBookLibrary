using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BigBookLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BorrowedOn", "DueDate" },
                values: new object[] { new DateTime(2026, 3, 28, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(5908), new DateTime(2026, 4, 11, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(5913) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BorrowedOn", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 14, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(5917), new DateTime(2026, 4, 28, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(5917) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BorrowedOn", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 12, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(5919), new DateTime(2026, 4, 26, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(5919) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BorrowedOn", "DueDate", "ReturnedOn" },
                values: new object[] { new DateTime(2026, 4, 5, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(5920), new DateTime(2026, 4, 19, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(5921), new DateTime(2026, 4, 15, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(5921) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BorrowedOn", "DueDate", "ReturnedOn" },
                values: new object[] { new DateTime(2026, 4, 2, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(5924), new DateTime(2026, 4, 16, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(5924), new DateTime(2026, 4, 14, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(5925) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "BorrowedOn", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 16, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(5926), new DateTime(2026, 4, 30, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(5927) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "BorrowedOn", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 13, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(5928), new DateTime(2026, 4, 27, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(5928) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "BorrowedOn", "DueDate", "ReturnedOn" },
                values: new object[] { new DateTime(2026, 3, 30, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(5930), new DateTime(2026, 4, 13, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(5930), new DateTime(2026, 4, 12, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(5931) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "BorrowedOn", "DueDate", "ReturnedOn" },
                values: new object[] { new DateTime(2026, 4, 10, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(5932), new DateTime(2026, 4, 24, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(5932), new DateTime(2026, 4, 16, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(5933) });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "BookId", "Comment", "CreatedOn", "Rating", "UserId" },
                values: new object[,]
                {
                    { 1, 2, "Amazing book! Highly recommended.", new DateTime(2026, 4, 7, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(6052), 5, "22f27b10-8477-4db4-a0f1-024e9e7e369d" },
                    { 2, 3, "Very interesting and well written.", new DateTime(2026, 4, 10, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(6055), 4, "22f27b10-8477-4db4-a0f1-024e9e7e369d" },
                    { 3, 4, "Good, but not great. Some parts were slow.", new DateTime(2026, 4, 14, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(6057), 3, "22f27b10-8477-4db4-a0f1-024e9e7e369d" },
                    { 4, 5, "Loved every chapter!", new DateTime(2026, 4, 5, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(6058), 5, "e21751ac-1fa9-41f8-9026-acf50ca8d9fb" },
                    { 5, 6, "Not my style, but others may enjoy it.", new DateTime(2026, 4, 11, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(6059), 2, "e21751ac-1fa9-41f8-9026-acf50ca8d9fb" },
                    { 6, 7, "Solid read with great characters.", new DateTime(2026, 4, 15, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(6060), 4, "e21751ac-1fa9-41f8-9026-acf50ca8d9fb" },
                    { 7, 8, "One of the best books in the library!", new DateTime(2026, 4, 2, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(6062), 5, "2527ff55-6195-49b4-8f13-4f420775862d" },
                    { 8, 9, "Great pacing and strong plot.", new DateTime(2026, 4, 8, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(6063), 4, "2527ff55-6195-49b4-8f13-4f420775862d" },
                    { 9, 10, "Decent book, but expected more.", new DateTime(2026, 4, 12, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(6065), 3, "2527ff55-6195-49b4-8f13-4f420775862d" },
                    { 10, 11, "Fantastic! A must-read.", new DateTime(2026, 4, 16, 12, 4, 23, 389, DateTimeKind.Utc).AddTicks(6066), 5, "2527ff55-6195-49b4-8f13-4f420775862d" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Reviews",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BorrowedOn", "DueDate" },
                values: new object[] { new DateTime(2026, 3, 28, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2206), new DateTime(2026, 4, 11, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2212) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BorrowedOn", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 14, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2215), new DateTime(2026, 4, 28, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2216) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BorrowedOn", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 12, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2217), new DateTime(2026, 4, 26, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2218) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BorrowedOn", "DueDate", "ReturnedOn" },
                values: new object[] { new DateTime(2026, 4, 5, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2219), new DateTime(2026, 4, 19, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2219), new DateTime(2026, 4, 15, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2220) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BorrowedOn", "DueDate", "ReturnedOn" },
                values: new object[] { new DateTime(2026, 4, 2, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2223), new DateTime(2026, 4, 16, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2223), new DateTime(2026, 4, 14, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2224) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "BorrowedOn", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 16, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2225), new DateTime(2026, 4, 30, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2225) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "BorrowedOn", "DueDate" },
                values: new object[] { new DateTime(2026, 4, 13, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2226), new DateTime(2026, 4, 27, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2227) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "BorrowedOn", "DueDate", "ReturnedOn" },
                values: new object[] { new DateTime(2026, 3, 30, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2228), new DateTime(2026, 4, 13, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2229), new DateTime(2026, 4, 12, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2229) });

            migrationBuilder.UpdateData(
                table: "Borrowings",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "BorrowedOn", "DueDate", "ReturnedOn" },
                values: new object[] { new DateTime(2026, 4, 10, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2231), new DateTime(2026, 4, 24, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2231), new DateTime(2026, 4, 16, 10, 0, 53, 482, DateTimeKind.Utc).AddTicks(2232) });
        }
    }
}
