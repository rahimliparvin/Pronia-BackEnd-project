using Microsoft.EntityFrameworkCore;
using ProniaBackEndProject.Data;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.Services.Interfaces;

namespace ProniaBackEndProject.Services
{
    public class TagService : ITagService
    {
        private readonly AppDbContext _context;
        public TagService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync() => await _context.Tags.Where(m => !m.SoftDelete). 
                                                                                 Include(m=>m.ProductTags).  
                                                                                 ThenInclude(m=>m.Product).
                                                                                 ToListAsync();

        public async Task<Tag> GetByIdAsync(int id) => await _context.Tags.FindAsync(id);
        
    }
}
