using ProniaBackEndProject.Helpers;
using ProniaBackEndProject.Model;

namespace ProniaBackEndProject.ViewModels
{
    public class BlogVM
    {
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
        public Blog Blog { get; set; }
        public IEnumerable<Banner> Banners { get; set; }
        public int ProductsCount { get; set; }

        public Paginate<Blog> PaginatedDatas { get; set; }
    }
}
