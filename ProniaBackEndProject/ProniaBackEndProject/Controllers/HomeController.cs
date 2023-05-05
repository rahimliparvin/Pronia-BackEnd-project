using Microsoft.AspNetCore.Mvc;
using ProniaBackEndProject.Data;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.Services.Interfaces;
using ProniaBackEndProject.ViewModels;

namespace ProniaBackEndProject.Controllers
{
    
    public class HomeController : Controller
    {

        private readonly ISliderService _sliderService;
        private readonly AppDbContext _context;
        public HomeController(ISliderService sliderService,
                              AppDbContext context)
        {
            _sliderService = sliderService; 
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Slider> sliders = await _sliderService.GetAll();

            HomeVM model = new()
            {
                Sliders = sliders
            };

            return View(model);
        }

    
    }
}