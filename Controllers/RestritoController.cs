using FazendaUrbana.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FazendaUrbana.Controllers
{
    // [PaginaUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
