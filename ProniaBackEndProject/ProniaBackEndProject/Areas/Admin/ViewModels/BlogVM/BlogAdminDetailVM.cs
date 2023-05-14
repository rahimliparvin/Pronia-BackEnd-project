using ProniaBackEndProject.Model;

namespace ProniaBackEndProject.Areas.Admin.ViewModels.BlogVM
{
    public class BlogAdminDetailVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public List<IFormFile> Photos { get; set; }
        public ICollection<BlogImage> Images { get; set; }
    }
}
