using ProniaBackEndProject.Model;

namespace ProniaBackEndProject.ViewModels
{
    public class ShopVM
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public IEnumerable<Size> Sizes { get; set; }
        public IEnumerable<Color> Colors { get; set; } 
    }
}
