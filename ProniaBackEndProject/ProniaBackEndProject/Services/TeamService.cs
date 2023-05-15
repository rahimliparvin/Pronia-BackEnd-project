using Microsoft.EntityFrameworkCore;
using ProniaBackEndProject.Data;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.Services.Interfaces;

namespace ProniaBackEndProject.Services
{
    public class TeamService : ITeamService
    {
        private readonly AppDbContext _context;
        public TeamService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Team>> GetAllAsync() => await _context.Teams.ToListAsync();

        public async Task<Team> GetByIdAsync(int id) => await _context.Teams.FindAsync(id);
        
    }
}
