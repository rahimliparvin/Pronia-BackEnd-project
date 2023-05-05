using Microsoft.EntityFrameworkCore;
using ProniaBackEndProject.Data;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.Services.Interfaces;

namespace ProniaBackEndProject.Services
{
    public class ClientService : IClientService
    {
        private readonly AppDbContext _context;
        public ClientService(AppDbContext context)
        {
            _context= context;
        }
        public async Task<IEnumerable<Client>> GetAllAsync() => await _context.Clients.Where(m => !m.SoftDelete).ToListAsync();


        public async Task<Client> GetByIdAsync(int id) => await _context.Clients.FindAsync(id);

    }
}
