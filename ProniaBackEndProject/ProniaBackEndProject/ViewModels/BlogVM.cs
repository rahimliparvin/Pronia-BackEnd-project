using ProniaBackEndProject.Model;

namespace ProniaBackEndProject.ViewModels
{
    public class BlogVM
    {
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public Blog Blog { get; set; }

    }
}
