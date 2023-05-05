using Microsoft.EntityFrameworkCore;
using ProniaBackEndProject.Model;

namespace ProniaBackEndProject.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }

    }
}
