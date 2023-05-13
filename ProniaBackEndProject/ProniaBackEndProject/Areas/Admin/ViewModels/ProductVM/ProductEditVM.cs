using ProniaBackEndProject.Model;
using System.ComponentModel.DataAnnotations;

namespace ProniaBackEndProject.Areas.Admin.ViewModels.ProductVM
{
    public class ProductEditVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public string Information { get; set; }
        [Required]
        public int Rates { get; set; }
        [Required]
        public int SaleCount { get; set; }
        [Required]
        public int StockCount { get; set; }
        [Required]
        public string Sku { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public List<IFormFile> Photos { get; set; }
        [Required]
        public List<int> ProductColorsIds { get; set; } = new();
        [Required]
        public List<int> ProductCategoriesIds { get; set; } = new();
        [Required]
        public List<int> ProductSizesIds { get; set; } = new();
        [Required]
        public List<int> ProductTagsIds { get; set; } = new();
    }
}
