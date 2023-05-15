using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaBackEndProject.Areas.Admin.ViewModels.ContactVM;
using ProniaBackEndProject.Data;
using ProniaBackEndProject.Helpers;
using ProniaBackEndProject.Model;

namespace ProniaBackEndProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        public ContactController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            IEnumerable<Contact> dbContacts = await _context.Contacts.ToListAsync();

            List<ContactIndexVM> contacts = new();

            foreach (var contact in dbContacts)
            {
                ContactIndexVM model = new()
                {
                    Id = contact.Id,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Email = contact.Email
                };

                contacts.Add(model);
            }
         
            return View(contacts);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null) return BadRequest();

            Contact contact = await _context.Contacts.FindAsync(id);

            if (contact == null) return NotFound();

			_context.Contacts.Remove(contact);

			await _context.SaveChangesAsync();

			return RedirectToAction(nameof(Index));

		}

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
			if (id == null) return BadRequest();

			Contact contact = await _context.Contacts.FindAsync(id);

			if (contact == null) return NotFound();

            ContactDetailVM model = new()
            {
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                Phone = contact.Phone,
                Message = contact.Message
            };

            return View(model);

		}
	}
}
