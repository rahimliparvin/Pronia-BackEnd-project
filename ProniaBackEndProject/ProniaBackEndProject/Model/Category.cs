namespace ProniaBackEndProject.Model
{
    public class Category : BaseEntity
    {
        public string Name { get;set; }
        public ICollection<ProductCategory> ProductCategories { get; set; }

    }
}
