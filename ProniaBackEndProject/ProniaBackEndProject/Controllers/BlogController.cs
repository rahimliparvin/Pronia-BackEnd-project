using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProniaBackEndProject.Controllers
{
    public class BlogController : Controller
    {
    
        public ActionResult Index()
        {
            return View();
        }

     
    }
}
