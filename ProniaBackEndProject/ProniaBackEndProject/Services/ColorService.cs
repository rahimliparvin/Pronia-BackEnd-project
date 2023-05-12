using Microsoft.EntityFrameworkCore;
using ProniaBackEndProject.Data;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.Services.Interfaces;

namespace ProniaBackEndProject.Services
{
    public class ColorService : IColorService
    {
        private readonly AppDbContext _context;
        public ColorService(AppDbContext context)
        {
            _context = context;  
        }
        public async Task<IEnumerable<Color>> GetAllAsync() => await _context.Colors.Include(m=>m.ProductColors).ThenInclude(m=>m.Product).ToListAsync();

        public async Task<Color> GetByIdAsync(int id) => await _context.Colors.FindAsync(id);
      
    }
}
