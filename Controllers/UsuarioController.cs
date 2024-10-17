using Microsoft.AspNetCore.Mvc;

namespace FazendaUrbana.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
