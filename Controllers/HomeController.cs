using FazendaUrbana.Filters;
using FazendaUrbana.Helper;
using FazendaUrbana.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FazendaUrbana.Controllers
{
    [PaginaUsuarioLogado]
    public class HomeController : Controller
    {
        private readonly ISessao _sessao;

        public HomeController(ISessao sessao)
        {
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            var usuarioLogado = _sessao.BuscarSessaoUsuario();
            ViewBag.UsuarioLogado = usuarioLogado;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
