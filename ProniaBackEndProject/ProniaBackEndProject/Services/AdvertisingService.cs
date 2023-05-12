using Microsoft.EntityFrameworkCore;
using ProniaBackEndProject.Data;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.Services.Interfaces;

namespace ProniaBackEndProject.Services
{
    public class AdvertisingService : IAdvertisingService
    {
        private readonly AppDbContext _context;
        public AdvertisingService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Advertising>> GetAllAsync() =>  await _context.Advertisings.Where(m => !m.SoftDelete).ToListAsync();

        public async Task<Advertising> GetFullDataByIdAsync(int id) => await _context.Advertisings.FindAsync(id);
        
    }
}
