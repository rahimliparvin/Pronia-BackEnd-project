using EntityFramework_Slider.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProniaBackEndProject.Data;
using ProniaBackEndProject.Helpers;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.Services;
using ProniaBackEndProject.Services.Interfaces;
using ProniaBackEndProject.ViewModels;

namespace ProniaBackEndProject.Controllers
{
    public class ShopController : Controller
    {
        private readonly AppDbContext _context;
        private IProductService _productService;
        private ICategoryService _categoryService;
        private ITagService _tagService;
        private ISizeService _sizeService;
        private IColorService _colorService;

        public ShopController(AppDbContext context,
                              IProductService productService, 
                              ICategoryService categoryService,
                              ITagService tagService,
                              ISizeService sizeService,
                              IColorService colorService)
        {
            _context = context;
            _productService = productService;
            _categoryService = categoryService;
            _tagService = tagService;
            _sizeService = sizeService;
            _colorService = colorService;
        }
        public async Task<IActionResult> Index(int page = 1, int take = 4)
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


        public async Task<IActionResult> Detail(int id)
        {
            if (id == null) return BadRequest();

            Product product = await _productService.GetByIdAsync(id);

            if (product == null) return NotFound();


            ProductDetailVM productDetailVM = new()
            {
                Name = product.Name,
                Description = product.Description,
                StockCount = product.StockCount,
                Price = product.Price,
                Rates = product.Rates,
                SKU = product.Sku,
                Information = product.Information,
                Images = product.ProductImages,
                Sizes = product.ProductSizes,
                Categories = product.ProductCategories,
                Tags = product.ProductTags,
                Colors = product.ProductColors

            };


            return View(productDetailVM);
        }


        //private async Task<int> GetPageCountAsync(int take)
        //{
        //    var productCount = await _productService.GetCountAsync();

        //    return (int)Math.Ceiling((decimal)productCount / take);
        //}


        ///Categorylerin klikinde productlari getirsin !
        [HttpGet]
        public async Task<IActionResult> GetCategoryProducts(int? id)
        {
            if (id == null) return BadRequest();

            var products = await _context.ProductCategories.Where(m => m.Category.Id == id).
                                                            Include(m => m.Product).
                                                            ThenInclude(m => m.ProductImages).
                                                            Select(m=>m.Product).
                                                            ToListAsync();
           

            return PartialView("_ProductsPartial",products); 
        }



        [HttpGet]
        public async Task<IActionResult> GetAllCategoriesProducts()
        {
            var products = await _context.Products.Include(m=>m.ProductImages).ToListAsync();



            return PartialView("_ProductsPartial", products);
        }
    }
}
