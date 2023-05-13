using Microsoft.EntityFrameworkCore;
using ProniaBackEndProject.Data;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.Services.Interfaces;

namespace ProniaBackEndProject.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Category>> GetAllAsync() => await _context.Categories.Include(m=>m.ProductCategories).
                                                                                            ThenInclude(m=>m.Product)?.      
                                                                                            ToListAsync();

        public async Task<Category> GetByIdAsync(int id) => await _context.Categories.FindAsync(id);
       
    }
}
