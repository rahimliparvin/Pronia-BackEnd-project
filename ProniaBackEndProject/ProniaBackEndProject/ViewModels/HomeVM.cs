using ProniaBackEndProject.Model;

namespace ProniaBackEndProject.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Advertising> Advertising { get; set; }
        public IEnumerable<Client> Clients { get; set; }   
        public IEnumerable<Brands> Brands { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
    }
}
