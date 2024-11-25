using FazendaUrbana.Helper;
using FazendaUrbana.Models;
using FazendaUrbana.Repositorio;
using FazendaUrbana.Helper;
using FazendaUrbana.Models;
using FazendaUrbana.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MeuSiteEmMVC.Controllers
{
    public class AlterarSenhaController : Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        public AlterarSenhaController(IUsuarioRepositorio usuarioRepositorio,
                                       ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            var usuarios = _usuarioRepositorio.BuscarTodos().Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = $"{u.Nome}"
            }).ToList();

            ViewBag.Usuarios = usuarios;
            return View();
        }

        [HttpPost]
        public IActionResult Alterar(AlterarSenhaModel alterarSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuario = _usuarioRepositorio.ListarPorId(alterarSenhaModel.Id);
                    if (usuario == null)
                    {
                        TempData["MensagemErro"] = "Usuário não encontrado!";
                        return RedirectToAction("Index");
                    }

                    _usuarioRepositorio.AlterarSenha(alterarSenhaModel);
                    TempData["MensagemSucesso"] = $"Senha de {usuario.Nome} alterada com sucesso!";
                    return View("Index", alterarSenhaModel);
                }
                return View("Index", alterarSenhaModel);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos alterar sua senha, tente novamente, detalhe do erro: {erro.Message}";
                return View("Index", alterarSenhaModel);
            }
        }
    }
}
