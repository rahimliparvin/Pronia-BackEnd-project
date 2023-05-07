using Microsoft.EntityFrameworkCore;
using ProniaBackEndProject.Data;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.Services.Interfaces;

namespace ProniaBackEndProject.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context= context;
        }
        public async Task<IEnumerable<Product>> GetAllAsync() => await _context.Products.Include(m => m.ProductImages).
                                                                                         Include(m => m.ProductCategories).
                                                                                         ThenInclude(m=>m.Category).
                                                                                         Include(m => m.ProductTags).
                                                                                         ThenInclude(m=>m.Tag).
                                                                                         Include(m => m.ProductColors).
                                                                                         ThenInclude(m=>m.Color).
                                                                                         Include(m => m.ProductSizes).
                                                                                         ThenInclude(m=>m.Size).
                                                                                         ToListAsync();
       

        public Task<Product> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
