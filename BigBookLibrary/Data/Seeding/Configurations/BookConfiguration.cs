using BigBookLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigBookLibrary.Data.Seeding.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasData(
                new Book
                {
                    Id = 2,
                    Title = "The Da Vinci Code",
                    Description = "A symbologist uncovers a conspiracy hidden in famous artworks.",
                    ISBN = "9780307474278",
                    CoverImagePath = "/images/books/davinci.jpg",
                    Year = 2003,
                    CopiesAvailable = 5,
                    AuthorId = 1,
                    GenreId = 1
                },
                new Book
                {
                    Id = 3,
                    Title = "Angels & Demons",
                    Description = "Robert Langdon investigates a murder linked to the Illuminati.",
                    ISBN = "9780743493468",
                    CoverImagePath = "/images/books/angels.jpg",
                    Year = 2000,
                    CopiesAvailable = 4,
                    AuthorId = 1,
                    GenreId = 1
                },
                new Book
                {
                    Id = 4,
                    Title = "Inferno",
                    Description = "A mystery tied to Dante’s Inferno threatens global catastrophe.",
                    ISBN = "9780804172264",
                    CoverImagePath = "/images/books/inferno.jpg",
                    Year = 2013,
                    CopiesAvailable = 6,
                    AuthorId = 1,
                    GenreId = 1
                },

                new Book
                {
                    Id = 5,
                    Title = "Harry Potter and the Philosopher's Stone",
                    Description = "The beginning of Harry Potter’s magical journey.",
                    ISBN = "9780747532699",
                    CoverImagePath = "/images/books/hp1.jpg",
                    Year = 1997,
                    CopiesAvailable = 10,
                    AuthorId = 2,
                    GenreId = 2
                },
                new Book
                {
                    Id = 6,
                    Title = "Harry Potter and the Chamber of Secrets",
                    Description = "Harry returns to Hogwarts to face a dark force.",
                    ISBN = "9780747538493",
                    CoverImagePath = "/images/books/hp2.jpg",
                    Year = 1998,
                    CopiesAvailable = 8,
                    AuthorId = 2,
                    GenreId = 2
                },
                new Book
                {
                    Id = 7,
                    Title = "Harry Potter and the Prisoner of Azkaban",
                    Description = "A dangerous fugitive threatens Hogwarts.",
                    ISBN = "9780747542155",
                    CoverImagePath = "/images/books/hp3.jpg",
                    Year = 1999,
                    CopiesAvailable = 7,
                    AuthorId = 2,
                    GenreId = 2
                },

                new Book
                {
                    Id = 8,
                    Title = "A Game of Thrones",
                    Description = "Noble families fight for control of the Iron Throne.",
                    ISBN = "9780553103540",
                    CoverImagePath = "/images/books/got1.jpg",
                    Year = 1996,
                    CopiesAvailable = 5,
                    AuthorId = 3,
                    GenreId = 3
                },
                new Book
                {
                    Id = 9,
                    Title = "A Clash of Kings",
                    Description = "The Seven Kingdoms descend into war.",
                    ISBN = "9780553108033",
                    CoverImagePath = "/images/books/got2.jpg",
                    Year = 1998,
                    CopiesAvailable = 5,
                    AuthorId = 3,
                    GenreId = 3
                },
                new Book
                {
                    Id = 10,
                    Title = "A Storm of Swords",
                    Description = "Betrayal and war reshape Westeros.",
                    ISBN = "9780553106633",
                    CoverImagePath = "/images/books/got3.jpg",
                    Year = 2000,
                    CopiesAvailable = 5,
                    AuthorId = 3,
                    GenreId = 3
                },

                new Book
                {
                    Id = 11,
                    Title = "Digital Fortress",
                    Description = "A cryptographer uncovers a threat to national security.",
                    ISBN = "9780312944926",
                    CoverImagePath = "/images/books/digital.jpg",
                    Year = 1998,
                    CopiesAvailable = 4,
                    AuthorId = 1,
                    GenreId = 1
                }
            );
        }
    }
}
