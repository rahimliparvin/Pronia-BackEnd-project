using ProniaBackEndProject.Model;

namespace ProniaBackEndProject.ViewModels
{
    public class ProductDetailVM
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int StockCount { get; set; }
        public decimal Price { get; set; }
        public int Rates { get; set; }
        public string SKU { get; set; }
        public string Information { get;set; }
        public ICollection<ProductCategory> Categories { get; set; }
        public ICollection<ProductColor> Colors { get; set; }
        public ICollection<ProductSize> Sizes { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<ProductTag> Tags { get; set; }
        
    }
}
