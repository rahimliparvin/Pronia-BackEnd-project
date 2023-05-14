using Microsoft.EntityFrameworkCore;
using ProniaBackEndProject.Data;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.Services.Interfaces;

namespace ProniaBackEndProject.Services
{
    public class BannerService : IBannerService
    {
        private readonly AppDbContext _context;

        public BannerService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Banner>> GetAllAsync() => await _context.Banners.Where(s => !s.SoftDelete).ToListAsync();


        public async Task<Banner> GetByIdAsync(int id) => await _context.Banners.FindAsync(id);

    }
}
