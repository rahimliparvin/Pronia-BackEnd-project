﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaBackEndProject.Helpers;
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
        private readonly IBannerService _bannerService;
        private readonly IProductService _productService;
        public BlogController(IBlogService blogService,
                              ICategoryService categoryService,
                              ITagService tagService,
                              IBannerService bannerService,
                              IProductService productService)
        {
            _blogService = blogService;
            _categoryService = categoryService;
            _tagService = tagService;
            _bannerService = bannerService;
            _productService = productService;
        }
    
     public async Task<IActionResult> Index(int page = 1, int take = 2)
        {
            IEnumerable<Banner> banners = await _bannerService.GetAllAsync();

            IEnumerable<Blog> blogs = await _blogService.GetAllAsync();

            IEnumerable<Category> categories = await _categoryService.GetAllAsync();

            IEnumerable<Tag> tags = await _tagService.GetAllAsync();

            int productsCount = await _productService.GetCountAsync();

            

            IEnumerable<Blog> dbproducts = await _blogService.GetPaginatedDatas(page, take);


            int pageCount = await GetPageCountAsync(take);


            Paginate<Blog> paginatedDatas = new(dbproducts, page, pageCount);


            BlogVM model = new()
            {
               
                Categories = categories,
                Tags = tags,
                Banners = banners,
                ProductsCount = productsCount,
                PaginatedDatas = paginatedDatas,

                Blogs = blogs
            };

            return View(model);


        }



        private async Task<int> GetPageCountAsync(int take)
        {
            var productCount = await _blogService.GetCountAsync();
            return (int)Math.Ceiling((decimal)productCount / take);


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
                AuthorName = blog.Author.Name,
                Created = blog.Created
                
            };

            return View(model);
        }

     
    }
}
