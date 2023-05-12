using Microsoft.AspNetCore.Mvc;
using ProniaBackEndProject.Areas.Admin.ViewModels;
using ProniaBackEndProject.Data;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.Services.Interfaces;

namespace ProniaBackEndProject.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class ClientController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;
        private readonly IClientService _clientService;

        public ClientController(IWebHostEnvironment env,
                                AppDbContext context,
                                IClientService clientService)
        {
            _env = env;
            _context = context;
            _clientService = clientService;

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Client> clients = await _clientService.GetAllAsync();

            List<ClientListIndexVM> model = new();

            foreach (var client in clients)
            {
                ClientListIndexVM mappedData = new()
                {
                    Id = client.Id,
                    Name = client.Name,
                    Description = client.Description,
                    Image = client.Image
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


    }
}
