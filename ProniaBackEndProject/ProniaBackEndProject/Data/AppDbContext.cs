using Microsoft.EntityFrameworkCore;
using ProniaBackEndProject.Model;

namespace ProniaBackEndProject.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Advertising> Advertisings { get; set; }    
        public DbSet<Client> Clients { get; set;}
        public DbSet<Brands> Brands { get; set; }   
        public DbSet<Author> Authors { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
        public DbSet<Blog> Blogs { get; set; }
    }
}
