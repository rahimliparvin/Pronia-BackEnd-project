using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.Services.Interfaces;
using ProniaBackEndProject.ViewModels;

namespace ProniaBackEndProject.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly ITagService _tagService;
        public BlogController(IBlogService blogService,
                              ICategoryService categoryService,
                              ITagService tagService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _tagService = tagService;
        }
    
        public async Task<ActionResult> Index()
        {

            IEnumerable<Blog> blogs = await _blogService.GetAllAsync();
            IEnumerable<Tag> tags = await _tagService.GetAllAsync();
            IEnumerable<Category> categories = await _categoryService.GetAllAsync();

            BlogVM model = new()
            {
                Blogs =  blogs,
                Tags = tags,
                Categories = categories
            };


            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if (id == null) return BadRequest();

            Blog blog = await _blogService.GetFullDataByIdAsync(id);

            if(blog == null) return NotFound();

            IEnumerable<Blog> blogs = await _blogService.GetAllAsync();
            IEnumerable<Tag> tags = await _tagService.GetAllAsync();
            IEnumerable<Category> categories = await _categoryService.GetAllAsync();


            BlogDetailVM model = new()
            {
                Blogs = blogs,
                Tags = tags,
                Categories = categories,
                Title = blog.Title,
                Description = blog.Description,
                BlogImages = blog.Images,
                AuthorName = blog.Author.Name
                
            };

            return View(model);
        }

     
    }
}
