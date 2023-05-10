using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.Services.Interfaces;
using ProniaBackEndProject.ViewModels.Account;

namespace ProniaBackEndProject.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IEmailService _emailService;
        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            AppUser newUser = new()
            {
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                RememberMe = model.RememberMe
            };

            IdentityResult result = await _userManager.CreateAsync(newUser, model.Password);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);

                }

                return View(model);
            }


            string token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);

            //link yaradiriq.bu link email'a gondereceyimiz a taginin href'ine qoyacagiq.Email'dan a tag'ina click  edende bizim saytimiza gelsin deye !
            //Bu linkin de bizim saytda gelmesi ucun asagidaki datalari bu linke qoyuruq !
            //Request.Scheme - saytin http/https -ini bize verir !
            //Request.Host.ToString() - Hostumuzu verir bize!
            //Objectin icinde userId ve tokeni qoyuruq !
            string link = Url.Action(nameof(ConfirmEmail), "Account", new { userId = newUser.Id, token }, Request.Scheme, Request.Host.ToString());

            string subject = "Register Confirmation";
            string html = string.Empty;

            using (StreamReader reader = new StreamReader("wwwroot/templates/verify.html"))
            {
                html = reader.ReadToEnd();
            }

            html = html.Replace("{{link}}", link);
            html = html.Replace("{{headerText}}", "Hello P135");
            _emailService.Send(newUser.Email, subject, html);

            // await _signInManager.SignInAsync(newUser, model.RememberMe);
            return RedirectToAction(nameof(VerifyEmail));

        }


        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null) return BadRequest();

            AppUser user = await _userManager.FindByIdAsync(userId);

            if (user == null) return NotFound();

            await _userManager.ConfirmEmailAsync(user, token);

            await _signInManager.SignInAsync(user, false);

            return RedirectToAction("Index", "Home");
        }

        //Registrasiya olandan sonra yeni bir sehive acilacaq bu action vasitesile ve ekranda yazilacaqki get email'a bax ! 
        public IActionResult VerifyEmail()
        {
            return View();
        }




        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            AppUser user = await _userManager.FindByEmailAsync(model.EmailOrUserName);

            if (user == null)
            {
                user = await _userManager.FindByNameAsync(model.EmailOrUserName);
            }

            if (user is null)
            {
                ModelState.AddModelError(string.Empty, "Email or Password is wrong !");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Email or Password is wrong !");
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }


    }


}
