using EntityFramework_Slider.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProniaBackEndProject.Areas.Admin.ViewModels.ProductVM;
using ProniaBackEndProject.Data;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.Services.Interfaces;
using System.Drawing;
using System.Security.Policy;
using Color = ProniaBackEndProject.Model.Color;
using Size = ProniaBackEndProject.Model.Size;

namespace ProniaBackEndProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IColorService _colorService;
        private readonly ISizeService _sizeService;
        private readonly ITagService _tagService;

        public ProductController(IWebHostEnvironment env,
                                 AppDbContext context,
                                 IProductService productService,
                                 ICategoryService categoryService,
                                 IColorService colorService,
                                 ISizeService sizeService,
                                 ITagService tagService)
        {
            _env = env;
            _context = context;
            _productService = productService;
            _colorService= colorService;
            _sizeService= sizeService;
            _tagService = tagService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> products = await _productService.GetAllAsync();

            List<ProductIndexVM> mappedDatas = new();

            foreach (var product in products)
            {
                ProductIndexVM model = new()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    ProductImage = product.ProductImages.Where(m=>m.IsMain).FirstOrDefault()?.Image,
                    CategoryName = product.ProductCategories.Select(m=>m.Category).FirstOrDefault()?.Name
                };

                mappedDatas.Add(model);
            }
            return View(mappedDatas);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {

            ViewBag.categories = await GetCategoriesAsync();
            ViewBag.colors = await GetColorsAsync();
            ViewBag.tags = await GetTagsAsync();
            ViewBag.sizes = await GetSizesAsync();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM model)
        {


            ViewBag.categories = await GetCategoriesAsync();
            ViewBag.colors = await GetColorsAsync();
            ViewBag.tags = await GetTagsAsync();
            ViewBag.sizes = await GetSizesAsync();


            if (!ModelState.IsValid)
            {
                return View(model);
            }



            IEnumerable<Product> products = await _productService.GetAllAsync();

            List<ProductSize> sizes = new();

            foreach (var sizeId in model.ProductSizesIds)
            {
                ProductSize size = new()
                {
                    SizeId = sizeId
                };

                sizes.Add(size);
            }

            List<ProductColor> colors = new();

            foreach (var colorId in model.ProductColorsIds)
            {
                ProductColor color = new()
                {
                    ColorId = colorId
                };

                colors.Add(color);
            }

            List<ProductTag> tags = new();

            foreach (var tagId in model.ProductTagsIds)
            {
                ProductTag tag = new()
                {
                   TagId = tagId
                };

                tags.Add(tag);
            }

            List<ProductCategory> categories = new();

            foreach (var categoryId in model.ProductCategoriesIds)
            {
                ProductCategory category = new()
                {
                   CategoryId = categoryId
                };

                categories.Add(category);
            }



            
            foreach (var photo in model.Photos)
            {
                if (!photo.CheckFileType("image/"))
                {

                    ModelState.AddModelError("Photos", "File type must be image");
                    return View();

                }


                if (photo.CheckFileSize(200))
                {

                    ModelState.AddModelError("Photos", "Photo size must be max 200Kb");
                    return View();

                }

            }


            List<ProductImage> productImages = new();


            foreach (var photo in model.Photos)
            {

                string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName;

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", fileName);

                await FileHelper.SaveFileAsync(path, photo);

                ProductImage productImage = new()
                {
                    Image = fileName
                };

                productImages.Add(productImage);
            }


            productImages.FirstOrDefault().IsMain = true;

            decimal convertedPrice = decimal.Parse(model.Price);

            Product newProduct = new()
            {
                Name = model.Name,
                Description = model.Description,
                Price = convertedPrice,
                Sku = model.Sku,
                Information = model.Information,
                Rates = model.Rates,
                SaleCount = model.SaleCount,
                StockCount = model.StockCount,
                ProductCategories = categories,
                ProductColors = colors,
                ProductSizes = sizes,
                ProductTags = tags,
                ProductImages = productImages,


            };

            await _context.ProductImages.AddRangeAsync(productImages);
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));


        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {

            ViewBag.categories = await GetCategoriesAsync();
            ViewBag.colors = await GetColorsAsync();
            ViewBag.tags = await GetTagsAsync();
            ViewBag.sizes = await GetSizesAsync();

            if (id == null) return BadRequest();

            Product product = await _productService.GetFullDataByIdAsync(id);

            if (product == null) return NotFound();

            ProductEditVM model = new()
            {
                Name = product.Name,
                Description = product.Description,
                Price = product.Price.ToString(),
                SaleCount = product.SaleCount,
                StockCount = product.StockCount,
                Information = product.Information,
                Rates = product.Rates,
                Sku = product.Sku,
                Images = product.ProductImages,
                ProductCategoriesIds = product.ProductCategories.Select(m => m.CategoryId).ToList(),
                ProductSizesIds = product.ProductSizes.Select(m => m.SizeId).ToList(),
                ProductTagsIds = product.ProductTags.Select(m => m.TagId).ToList(),
                ProductColorsIds = product.ProductColors.Select(m => m.ColorId).ToList()

            };


               return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ProductEditVM model)
        {
            if (id == null) return BadRequest();

            Product dbProduct = await _productService.GetFullDataByIdAsync((int)id);

            if (dbProduct == null) return NotFound();

            ViewBag.categories = await GetCategoriesAsync();
            ViewBag.colors = await GetColorsAsync();
            ViewBag.tags = await GetTagsAsync();
            ViewBag.sizes = await GetSizesAsync();


            if (!ModelState.IsValid)
            {
                return View(model);
            }


            List<ProductCategory> categories = new();

            foreach (var categoryId in model.ProductCategoriesIds)
            {
                ProductCategory category = new()
                {
                    CategoryId = categoryId
                };

                categories.Add(category);
            }

            List<ProductColor> colors = new();

            foreach (var colorId in model.ProductColorsIds)
            {
                ProductColor color = new()
                {
                    ColorId = colorId
                };

                colors.Add(color);
            }

            List<ProductSize> sizes = new();

            foreach (var sizeId in model.ProductSizesIds)
            {
                ProductSize size = new()
                {
                    SizeId = sizeId
                };

                sizes.Add(size);
            }

            List<ProductTag> tags = new();

            foreach (var tagId in model.ProductTagsIds)
            {
                ProductTag tag = new()
                {
                    TagId = tagId
                };

                tags.Add(tag);
            }


            List<ProductImage> productImages = new();

            if (model.Photos != null)
            {

                foreach (var item in model.Photos)
                {

                    string fileName = Guid.NewGuid().ToString() + "_" + item.FileName;

                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", fileName);

                    await FileHelper.SaveFileAsync(path, item);

                    ProductImage productImage = new()
                    {
                        Image = fileName
                    };

                    productImages.Add(productImage);


                }

                foreach (var item in dbProduct.ProductImages)
                {

                    string path = FileHelper.GetFilePath(_env.WebRootPath,"assets/images/website-images", item.Image);

                    FileHelper.DeleteFile(path);
                }


                productImages.FirstOrDefault().IsMain = true;

                dbProduct.ProductImages = productImages;

            }
            else
            {
                dbProduct.ProductImages = dbProduct.ProductImages;
            }

           

            decimal convertedPrice = decimal.Parse(model.Price);

            dbProduct.Name = model.Name;
            dbProduct.Description = model.Description;
            dbProduct.Price = convertedPrice;
            dbProduct.Sku = model.Sku;
            dbProduct.Rates = model.Rates;
            dbProduct.Information = model.Information;
            dbProduct.SaleCount = model.SaleCount;
            dbProduct.StockCount = model.StockCount;
            dbProduct.ProductCategories = categories;
            dbProduct.ProductColors = colors;
            dbProduct.ProductSizes = sizes;
            dbProduct.ProductTags = tags;
            dbProduct.Updated = DateTime.Now;

            


            await _context.ProductImages.AddRangeAsync(productImages);
            _context.Products.Update(dbProduct);
            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));

          
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            Product product = await _productService.GetFullDataByIdAsync((int)id);

            if (product is null) return NotFound();


            foreach (var item in product.ProductImages)
            {
                string path = FileHelper.GetFilePath(_env.WebRootPath,"assets/images/website-images", item.Image);
                FileHelper.DeleteFile(path);

            }


            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }


        public async Task<SelectList> GetCategoriesAsync()
        {
            IEnumerable<Category> categories = await _categoryService.GetAllAsync();
            return new SelectList(categories, "Id", "Name");
        }

        public async Task<SelectList> GetSizesAsync()
        {
            IEnumerable<Size> sizes = await _sizeService.GetAllAsync();
            return new SelectList(sizes, "Id", "Name");
        }

        public async Task<SelectList> GetTagsAsync()
        {
            IEnumerable<Tag> tags = await _tagService.GetAllAsync();
            return new SelectList(tags, "Id", "Name");
        }

        public async Task<SelectList> GetColorsAsync()
        {
            IEnumerable<Color> colors = await _colorService.GetAllAsync();
            return new SelectList(colors, "Id", "Name");
        }
    }
}
