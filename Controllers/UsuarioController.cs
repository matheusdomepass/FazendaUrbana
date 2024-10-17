using Microsoft.AspNetCore.Mvc;

namespace ControleContratos.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
