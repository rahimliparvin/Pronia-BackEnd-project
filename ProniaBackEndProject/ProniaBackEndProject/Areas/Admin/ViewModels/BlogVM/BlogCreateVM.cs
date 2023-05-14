using System.ComponentModel.DataAnnotations;

namespace ProniaBackEndProject.Areas.Admin.ViewModels.BlogVM
{
    public class BlogCreateVM
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public int AuthorId { get; set; }

        [Required]
        public List<IFormFile> Photos { get; set; }
    }
}
