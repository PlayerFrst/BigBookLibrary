using BigBookLibrary.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BigBookLibrary.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
