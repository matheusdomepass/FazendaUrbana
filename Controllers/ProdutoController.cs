using FazendaUrbana.Models;
using FazendaUrbana.Helper;
using FazendaUrbana.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FazendaUrbana.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IHttpContextAccessor _contextAccessor;
        public ProdutoController(IProdutoRepositorio produtoRepositorio, IHttpContextAccessor contextAccessor)
        {
            _produtoRepositorio = produtoRepositorio;
            _contextAccessor = contextAccessor;
        }
        public IActionResult Index()
        {
            List<ProdutoModel> produtos = _produtoRepositorio.BuscarTodos();
            return View(produtos);
        }
        
        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            ProdutoModel produto = _produtoRepositorio.ListarPorId(id);
            return View(produto);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ProdutoModel produto = _produtoRepositorio.ListarPorId(id);
            return View(produto);
        }
        
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _produtoRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Produto apagado com sucesso";
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, não conseguimos apagar o produto";
                }
                return RedirectToAction("Index");

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar o produto, mais detalhes do erro {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(ProdutoModel produto)
        {
            try
            {
                string usuarioJson = _contextAccessor.HttpContext.Session.GetString("sessaoUsuarioLogado");

                if (string.IsNullOrEmpty(usuarioJson))
                {
                    TempData["MensagemErro"] = "Usuário não está logado";
                    return RedirectToAction("Index");
                }

                var usuario = JsonConvert.DeserializeObject<UsuarioModel>(usuarioJson);

                produto.Add_Por = usuario.Nome;

                if (ModelState.IsValid)
                {
                    _produtoRepositorio.Adicionar(produto);
                    TempData["MensagemSucesso"] = "Produto cadastrado com sucesso";
                    return RedirectToAction("Index");
                }

                return View(produto);

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o produto, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public IActionResult Alterar(ProdutoModel produto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _produtoRepositorio.Atualizar(produto);
                    TempData["MensagemSucesso"] = "Produto alterado com sucesso";
                    return RedirectToAction("Index");
                }
                return View("Editar", produto);

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos alterar o produto, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
        
    }
}

