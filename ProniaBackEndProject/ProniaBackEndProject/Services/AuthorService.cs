using Microsoft.EntityFrameworkCore;
using ProniaBackEndProject.Areas.Admin.Controllers;
using ProniaBackEndProject.Data;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.Services.Interfaces;

namespace ProniaBackEndProject.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _context;
        public AuthorService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Author>> GetAllAsync() => await _context.Authors.Include(m => m.Blogs).ToListAsync();

        public async Task<Author> GetByIdAsync(int id) => await _context.Authors.FindAsync(id);
      
    }
}
