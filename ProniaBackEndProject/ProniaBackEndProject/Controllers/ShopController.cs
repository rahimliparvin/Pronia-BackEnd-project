using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProniaBackEndProject.Controllers
{
    public class ShopController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }

       
    }
}
