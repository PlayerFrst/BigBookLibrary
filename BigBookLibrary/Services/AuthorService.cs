using BigBookLibrary.Areas.Admin.ViewModels.Authors;
using BigBookLibrary.Data;
using BigBookLibrary.Models;
using BigBookLibrary.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BigBookLibrary.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AuthorViewModel>> GetAllAsync()
        {
            return await _context.Authors
                .Where(а => !а.IsDeleted)
                .Select(a => new AuthorViewModel
                {
                    Id = a.Id,
                    Name = a.Name
                })
                .ToListAsync();
        }

        public async Task<AuthorFormModel?> GetByIdAsync(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
                return null;

            return new AuthorFormModel
            {
                Name = author.Name,
                Biography = author.Biography
            };
        }

        public async Task CreateAsync(AuthorFormModel model)
        {
            var author = new Author
            {
                Name = model.Name,
                Biography = model.Biography
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(int id, AuthorFormModel model)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
                return;

            author.Name = model.Name;
            author.Biography = model.Biography;

            await _context.SaveChangesAsync();
        }

        public async Task SoftDeleteAsync(int id)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
                return;

            author.IsDeleted = true;

            await _context.SaveChangesAsync();
        }
    }
}
