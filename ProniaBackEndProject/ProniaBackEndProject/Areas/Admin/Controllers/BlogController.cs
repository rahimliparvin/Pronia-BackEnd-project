using EntityFramework_Slider.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProniaBackEndProject.Areas.Admin.ViewModels.BlogVM;
using ProniaBackEndProject.Data;
using ProniaBackEndProject.Helpers;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.Services.Interfaces;
using ProniaBackEndProject.ViewModels;
using System.Data;
using System.Text.RegularExpressions;
using BlogDetailVM = ProniaBackEndProject.Areas.Admin.ViewModels.BlogVM.BlogAdminDetailVM;

namespace ProniaBackEndProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly IBlogService _blogService;
        private readonly IAuthorService _authorService;

        public BlogController(AppDbContext context, IWebHostEnvironment env, IBlogService blogService, IAuthorService authorService)
        {
            _context = context;
            _env = env;
            _blogService = blogService;
            _authorService = authorService;
        }


        public async Task<IActionResult> Index(int page = 1, int take = 5)
        {
            IEnumerable<Blog> blogs = await _blogService.GetPaginatedDatas(page, take);

            IEnumerable<BlogIndexVM> mappedDatas = GetMappedDatas(blogs);

            int pageCount = await GetPageCountAsync(take);

            Paginate<BlogIndexVM> paginatedDatas = new(mappedDatas, page, pageCount);

            ViewBag.take = take;

            return View(paginatedDatas);
        }

        //paglerin sayini veren method

        private async Task<int> GetPageCountAsync(int take)
        {
            var productCount = await _blogService.GetCountAsync();
            return (int)Math.Ceiling((decimal)productCount / take);
        }

        // pasingation method 
        private IEnumerable<BlogIndexVM> GetMappedDatas(IEnumerable<Blog> blogs)
        {
            List<BlogIndexVM> mappedDatas = new();

            foreach (var blog in blogs)
            {
                BlogIndexVM prodcutVM = new()
                {
                    Id = blog.Id,
                    Title = blog.Title,
                    Description = blog.Description,
                    AuthorName = blog.Author.Name,
                    MainImage = blog.Images.Where(m => m.IsMain).FirstOrDefault()?.Image
                };

                mappedDatas.Add(prodcutVM);

            }
            return mappedDatas;
        }



        [HttpGet]
        public async Task<IActionResult> Create()
        {


            ViewBag.author = await GetAuthorsAsync();

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogCreateVM model)
        {
            try
            {


                ViewBag.author = await GetAuthorsAsync();

                if (!ModelState.IsValid)
                {
                    return View(model); 
                }


                foreach (var photo in model.Photos)
                {

                    if (!photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View();
                    }

                    if (photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View();
                    }



                }

                List<BlogImage> blogImages = new();  

                foreach (var photo in model.Photos)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName; 

                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", fileName);

                    await FileHelper.SaveFileAsync(path, photo);


                    BlogImage blogImage = new()  
                    {
                        Image = fileName
                    };

                    blogImages.Add(blogImage);

                }

                blogImages.FirstOrDefault().IsMain = true; 



                Blog newBlog = new()
                {
                    Title = model.Title,
                    Description = model.Description,
                    AuthorId = model.AuthorId,
                    Images = blogImages
                };

                await _context.BlogImages.AddRangeAsync(blogImages); 
                await _context.Blogs.AddAsync(newBlog);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {

                throw;
            }
        }




        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return BadRequest();

            Blog dbProduct = await _blogService.GetFullDataByIdAsync((int)id);

            if (dbProduct is null) return NotFound();

            ViewBag.author = await GetAuthorsAsync();

           


            return View(new BlogEditVM
            {
                Title = dbProduct.Title,
                Description = dbProduct.Description,
                AuthorId = dbProduct.AuthorId,
                Images = dbProduct.Images
            });


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, BlogEditVM model)
        {
            ViewBag.author = await GetAuthorsAsync();

            if (!ModelState.IsValid) return View(model);

            Blog dbBlog = await _blogService.GetFullDataByIdAsync((int)id);

            if (dbBlog is null) return NotFound();

            if (model.Photos != null)
            {

                foreach (var photo in model.Photos)
                {

                    if (!photo.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Photo", "File type must be image");
                        return View(dbBlog);
                    }

                    if (photo.CheckFileSize(200))
                    {
                        ModelState.AddModelError("Photo", "Image size must be max 200kb");
                        return View(dbBlog);
                    }



                }

                foreach (var item in dbBlog.Images)
                {

                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", item.Image);

                    FileHelper.DeleteFile(path);
                }



                List<BlogImage> productImages = new();  

                foreach (var photo in model.Photos)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + photo.FileName; 


                    string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", fileName);

                    await FileHelper.SaveFileAsync(path, photo);


                    BlogImage productImage = new()   
                    {
                        Image = fileName
                    };

                    productImages.Add(productImage);

                }

                productImages.FirstOrDefault().IsMain = true;

                dbBlog.Images = productImages;
            }



            dbBlog.Title = model.Title;
            dbBlog.Description = model.Description;
            dbBlog.AuthorId = model.AuthorId;

            await _context.SaveChangesAsync();


            return RedirectToAction(nameof(Index));
        }




        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            //ViewBag.author = await GetAuthorsAsync();

            Blog dbBlog = await _blogService.GetFullDataByIdAsync((int)id);

            //ViewBag.desc = Regex.Replace(dbBlog.Description, "<.*?>", String.Empty);

            return View(new BlogDetailVM   
            {

                Title = dbBlog.Title,
                Description = dbBlog.Description,
                AuthorId = dbBlog.AuthorId,
                Images = dbBlog.Images,
                AuthorName = dbBlog.Author.Name
            });
        }



        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            Blog product = await _blogService.GetFullDataByIdAsync((int)id);

            if (product is null) return NotFound();


            foreach (var item in product.Images)
            {

                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", item.Image);

                FileHelper.DeleteFile(path);

            }


            _context.Blogs.Remove(product);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        private async Task<SelectList> GetAuthorsAsync()
        {

            IEnumerable<Author> categories = await _authorService.GetAllAsync();

            return new SelectList(categories, "Id", "Name"); 


        }

    }
}


