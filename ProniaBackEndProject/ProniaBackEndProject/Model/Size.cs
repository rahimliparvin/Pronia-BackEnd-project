namespace ProniaBackEndProject.Model
{
    public class Size : BaseEntity
    {
        public string Name { get;set; }

        public ICollection<ProductSize> ProductSizes { get; set; }
        
    }
}
