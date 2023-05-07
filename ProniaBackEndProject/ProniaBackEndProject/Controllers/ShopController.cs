using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.Services;
using ProniaBackEndProject.Services.Interfaces;
using ProniaBackEndProject.ViewModels;

namespace ProniaBackEndProject.Controllers
{
    public class ShopController : Controller
    {
        private IProductService _productService;
        private ICategoryService _categoryService;
        private ITagService _tagService;
        private ISizeService _sizeService;
        private IColorService _colorService;
        public ShopController(IProductService productService, 
                              ICategoryService categoryService,
                              ITagService tagService,
                              ISizeService sizeService,
                              IColorService colorService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _tagService = tagService;
            _sizeService = sizeService;
            _colorService = colorService;
        }
        public async Task<IActionResult> Index()
        {

            IEnumerable<Product> products = await _productService.GetAllAsync();
            IEnumerable<Category> categories = await _categoryService.GetAllAsync();
            IEnumerable<Tag> tags = await _tagService.GetAllAsync();
            IEnumerable<Size> sizes = await _sizeService.GetAllAsync();
            IEnumerable<Color> colors = await _colorService.GetAllAsync();


            ShopVM model = new()
            {
                Products = products,
                Categories = categories,
                Tags = tags,
                Sizes = sizes,
                Colors = colors

            };

            return View(model);
        }

       
    }
}
