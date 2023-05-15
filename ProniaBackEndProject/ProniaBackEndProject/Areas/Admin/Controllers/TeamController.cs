using EntityFramework_Slider.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProniaBackEndProject.Areas.Admin.ViewModels.TagVM;
using ProniaBackEndProject.Areas.Admin.ViewModels.TeamVM;
using ProniaBackEndProject.Data;
using ProniaBackEndProject.Helpers;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.Services.Interfaces;

namespace ProniaBackEndProject.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class TeamController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly ITeamService _teamService;
        public TeamController(AppDbContext context, IWebHostEnvironment env, ITeamService teamService)
        {
            _context = context;
            _env = env;
            _teamService = teamService;
        }


        public async Task<IActionResult> Index()
        {
            IEnumerable<Team> teams = await _teamService.GetAllAsync();
            return View(teams);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TeamCreateVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!model.Photo.CheckFileType("image/"))
            {

                ModelState.AddModelError("Photo", "File type must be image");
                return View();

            }


            if (model.Photo.CheckFileSize(200))
            {

                ModelState.AddModelError("Photo", "Photo size must be max 200Kb");
                return View();

            }

            string fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;

            string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", fileName);

            await FileHelper.SaveFileAsync(path, model.Photo);

            Team newTeam = new()
            {
                Image = fileName,
                Name = model.Name,
                Position = model.Position

            };

            await _context.Teams.AddAsync(newTeam);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));


        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Team team = await _teamService.GetByIdAsync(id);

            _context.Teams.Remove(team);

            string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", team.Image);

            FileHelper.DeleteFile(path);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Team  team = await _teamService.GetByIdAsync(id);

            TeamEditVM model = new()
            {
                Name = team.Name,
                Position = team.Position,
                Image = team.Image
            };


            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TeamEditVM model)
        {
            if (id == null) return BadRequest();

            Team dbTeam = await _context.Teams.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (dbTeam == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Photo != null)
            {
                if (!model.Photo.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Photo", "File type must be image");
                    return View(model);
                }

                if (model.Photo.CheckFileSize(200))
                {
                    ModelState.AddModelError("Photo", "Photo size must be max 200Kb");
                    return View(model);
                }

                string fileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;


                string path = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", fileName);


                using (FileStream stream = new(path, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream);
                }


                string expath = FileHelper.GetFilePath(_env.WebRootPath, "assets/images/website-images", dbTeam.Image);


                FileHelper.DeleteFile(expath);

				dbTeam.Image = fileName;
            }
            else
            {
				dbTeam.Image = dbTeam.Image;
            }


			dbTeam.Name = model.Name;
            dbTeam.Position = model.Position;


            _context.Teams.Update(dbTeam);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }



        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            if (id == null) return BadRequest();

            Team team = await _teamService.GetByIdAsync(id);

            if (team == null) return NotFound();

            TeamDetailVM model = new()
            {
                Name = team.Name,
                Position  = team.Position,
                Image = team.Image,

            };

            return View(model);
        }




    }
}
