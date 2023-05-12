using System.ComponentModel.DataAnnotations.Schema;

namespace ProniaBackEndProject.Model
{
    public class Slider : BaseEntity
    {
        public string Offer { get; set; }
        public string Title { get; set;}
        public string Description { get; set;}
        public string Image { get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public string BackGroundImage { get; set; }
        [NotMapped]
        public IFormFile BackGroundPhoto { get; set; }
    }
}
