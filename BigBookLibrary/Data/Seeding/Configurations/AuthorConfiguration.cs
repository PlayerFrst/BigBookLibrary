using BigBookLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.HasData(
            new Author
            {
                Id = 1,
                Name = "Dan Brown",
                Biography = "Dan Brown is an American author best known for his thriller novels, including The Da Vinci Code and Angels & Demons."
            },
            new Author
            {
                Id = 2,
                Name = "J. K. Rowling",
                Biography = "J. K. Rowling is a British author famous for writing the Harry Potter series, one of the best-selling book franchises in history."
            },
            new Author
            {
                Id = 3,
                Name = "George R. R. Martin",
                Biography = "George R. R. Martin is an American novelist and short story writer, best known for the epic fantasy series A Song of Ice and Fire."
            }
        );
    }
}
