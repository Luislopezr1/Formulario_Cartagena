using Microsoft.AspNetCore.Mvc;

namespace Formulario_Cartagena.Controllers_Views
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
