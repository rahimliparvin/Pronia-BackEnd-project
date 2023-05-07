namespace ProniaBackEndProject.Model
{
    public class Color : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<ProductColor> ProductColors { get; set; } 

    }
}

