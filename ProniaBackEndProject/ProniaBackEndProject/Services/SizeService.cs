using Microsoft.EntityFrameworkCore;
using ProniaBackEndProject.Data;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.Services.Interfaces;

namespace ProniaBackEndProject.Services
{
    public class SizeService : ISizeService
    {
        private readonly AppDbContext _context;
        public SizeService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Size>> GetAllAsync() => await _context.Sizes.Include(m=>m.ProductSizes).  
                                                                                   ThenInclude(m=>m.Product).
                                                                                   Where(m=>!m.SoftDelete). 
                                                                                   ToListAsync();

        public async Task<Size> GetByIdAsync(int id) => await _context.Sizes.FindAsync(id);
       
    }
}
