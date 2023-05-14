using ProniaBackEndProject.Model;
using System.ComponentModel.DataAnnotations;

namespace ProniaBackEndProject.Areas.Admin.ViewModels.BlogVM
{
    public class BlogEditVM
    {

        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public ICollection<BlogImage> Images { get; set; }
        public List<IFormFile> Photos { get; set; }
    }
}
