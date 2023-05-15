using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaBackEndProject.Data;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.ViewModels;

namespace ProniaBackEndProject.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ContactVM model = new();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(ContactVM model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
              
            }

            Contact contact = new()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Phone = model.Phone,
                Message = model.Message
            };


            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();



            return RedirectToAction(nameof(Index));

        }
       
    }
}
