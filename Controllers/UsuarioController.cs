using FazendaUrbana.Filters;
using FazendaUrbana.Models;
using FazendaUrbana.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FazendaUrbana.Controllers
{
    [PaginaAdmin]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IHttpContextAccessor _contextAcessor;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, IHttpContextAccessor contextAcessor)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _contextAcessor = contextAcessor;
        }
        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();
            return View(usuarios);
        }
        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }
        public IActionResult ApagarConfirmacao(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            return View(usuario);
        }
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _usuarioRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Usuário apagado com sucesso";
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, não conseguimos apagar seu usuário";
                }
                return RedirectToAction("Index");

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar seu usuário, mais detalhes do erro {erro.Message}";
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult Criar(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var usuarioExistente = _usuarioRepositorio.BuscarPorLogin(usuario.Login);
                    if(usuarioExistente != null)
                    {
                        TempData["MensagemErro"] = "Nome de usuário já está cadastrado!";
                        return View(usuario);
                    }

                    var cpfExistente = _usuarioRepositorio.BuscarPorCPF(usuario.CPF);
                    if(cpfExistente != null)
                    {
                        TempData["MensagemErro"] = "CPF do usuário já está cadastrado!";
                        return View(usuario);
                    }

                    _usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu usuário, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public IActionResult Alterar(UsuarioSemSenhaModel usuarioSemSenhaModel)
        {
            try
            {
                UsuarioModel usuario = null;
                if (ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        Id = usuarioSemSenhaModel.Id,
                        Nome = usuarioSemSenhaModel.Nome,
                        Login = usuarioSemSenhaModel.Login,
                        Email = usuarioSemSenhaModel.Email,
                        Perfil = usuarioSemSenhaModel.Perfil,
                        CPF = usuarioSemSenhaModel.CPF,
                        DataNascimento = usuarioSemSenhaModel.DataNascimento
                    };
                    var usuarioExistente = _usuarioRepositorio.BuscarPorLogin(usuario.Login);
                    if (usuarioExistente != null && usuarioExistente.Id != usuario.Id)
                    {
                        TempData["MensagemErro"] = "Nome de usuário já está cadastrado!";
                        return View("Editar", usuario);
                    }

                    var cpfExistente = _usuarioRepositorio.BuscarPorCPF(usuario.CPF);
                    if (cpfExistente != null && cpfExistente.Id != usuario.Id)
                    {
                        TempData["MensagemErro"] = "CPF do usuário já está cadastrado!";
                        return View("Editar", usuario);
                    }
                    usuario = _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuário alterado com sucesso";
                    return RedirectToAction("Index");
                }
                return View("Editar", usuario);

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos alterar seu usuário, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
