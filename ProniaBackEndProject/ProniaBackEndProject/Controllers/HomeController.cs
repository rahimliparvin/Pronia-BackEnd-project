using Microsoft.AspNetCore.Mvc;
using ProniaBackEndProject.Data;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.Services.Interfaces;
using ProniaBackEndProject.ViewModels;

namespace ProniaBackEndProject.Controllers
{
    
    public class HomeController : Controller
    {

        private readonly AppDbContext _context;
        private readonly ISliderService _sliderService;
        private readonly IAdvertisingService _advertisingService;
        private readonly IClientService _clientService;
        private readonly IBrandService _brandService;
        private readonly IBlogService _blogService;
       
        
        public HomeController(ISliderService sliderService,
                              IAdvertisingService advertisingService,
                              IClientService clientService,
                              IBrandService brandService,
                              IBlogService blogService,
                              AppDbContext context)
        {
            _context = context;
            _sliderService = sliderService;
            _advertisingService = advertisingService;
            _clientService = clientService;
            _brandService = brandService;
            _blogService = blogService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Slider> sliders = await _sliderService.GetAllAsync();
            IEnumerable<Advertising> advertisings = await _advertisingService.GetAllAsync();
            IEnumerable<Client> clients = await _clientService.GetAllAsync();
            IEnumerable<Brand> brands = await _brandService.GetAllAsync();
            IEnumerable<Blog> blogs = await _blogService.GetAllAsync();

            HomeVM model = new()
            {
                Sliders = sliders,
                Advertising = advertisings,
                Clients = clients,
                Brands = brands,
                Blogs = blogs
                
                
            };

            return View(model);
        }

    
    }
}