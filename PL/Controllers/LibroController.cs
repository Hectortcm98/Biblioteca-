using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class LibroController : Controller
    {
        public IActionResult GetAll ()
        {
            return View();
        }
    }
}
