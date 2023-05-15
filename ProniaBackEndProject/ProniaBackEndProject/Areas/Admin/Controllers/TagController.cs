using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaBackEndProject.Areas.Admin.ViewModels.TagVM;
using ProniaBackEndProject.Data;
using ProniaBackEndProject.Helpers;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.Services.Interfaces;

namespace ProniaBackEndProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class TagController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        private readonly ITagService _tagService;

        public TagController(AppDbContext context,
                             ITagService tagService,
                                IWebHostEnvironment env)
        {
            _env = env;
            _context = context;
            _tagService = tagService;

        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Tag> tags = await _tagService.GetAllAsync();

            List<TagIndexVM> model = new();

            foreach (var tag in tags)
            {
                TagIndexVM mappedData = new()
                {
                    Id = tag.Id,
                    Name = tag.Name,
                };

                model.Add(mappedData);
            }

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TagCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Tag tag = new()
            {
                Name = model.Name
            };

            await _context.Tags.AddAsync(tag);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Tag tag = await _tagService.GetByIdAsync(id);

            _context.Tags.Remove(tag);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Tag tag = await _tagService.GetByIdAsync(id);

            TagEditVM model = new()
            {
                Name = tag.Name
            };


            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TagEditVM model)
        {
            if (id == null) return BadRequest();

            Tag tag = await _context.Tags.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (tag == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            tag.Name = model.Name;


            _context.Tags.Update(tag);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }

}
