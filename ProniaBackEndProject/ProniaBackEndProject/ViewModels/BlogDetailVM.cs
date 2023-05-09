using ProniaBackEndProject.Model;

namespace ProniaBackEndProject.ViewModels
{
    public class BlogDetailVM
    {
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Category> Categories { get; set;}
        public IEnumerable<Tag> Tags { get; set; }
        public ICollection<BlogImage> BlogImages { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public DateTime Created { get; set; }




    }
}
