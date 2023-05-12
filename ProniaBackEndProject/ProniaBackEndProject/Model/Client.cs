using System.ComponentModel.DataAnnotations.Schema;

namespace ProniaBackEndProject.Model
{
    public class Client: BaseEntity
    {
        public string Image{ get; set; }
        [NotMapped]
        public IFormFile Photo { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } 
    }
}
