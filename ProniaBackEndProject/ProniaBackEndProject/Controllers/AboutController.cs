using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProniaBackEndProject.Model;
using ProniaBackEndProject.Services.Interfaces;
using ProniaBackEndProject.ViewModels;

namespace ProniaBackEndProject.Controllers
{
    public class AboutController : Controller
    {
        private readonly ITeamService _teamService;
        public AboutController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public async  Task<ActionResult> Index()
        {
            IEnumerable<Team> teams = await _teamService.GetAllAsync();

            AboutVM model = new()
            {
                Teams = teams
            };

            return View(model);
        }

    }
}
