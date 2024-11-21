using FazendaUrbana.Models;
using FazendaUrbana.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FazendaUrbana.Controllers
{
    public class VendasController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IVendaRepositorio _vendaRepositorio;

        public VendasController(IProdutoRepositorio produtoRepositorio, IVendaRepositorio vendaRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
            _vendaRepositorio = vendaRepositorio;
        }
        public IActionResult Index()
        {
            List<VendasModel> vendas = _vendaRepositorio.BuscarTodos();
            return View(vendas);
        }
        public IActionResult Vender()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Vender(int produtoId, int quantidade, decimal imposto, decimal desconto)
        {
            var produto = _produtoRepositorio.ListarPorId(produtoId);
            if (produto == null)
            {
                TempData["MensagemErro"] = "Produto não encontrado!";
                return RedirectToAction("Index");
            }
            ViewBag.produtoId = produtoId;

            var valorTotal = produto.Valor * quantidade;
            var venda = new VendasModel
            {
                ProdutoId = produtoId,
                Quantidade = quantidade,
                ValorTotal = valorTotal,
                DataVenda = DateTime.Now,
            };
            var usuarioLogado = User.FindFirst("NomeUsuario")?.Value;

            var transacao = new TransacaoModel
            {
                Tipo = "Venda",
                Quantidade = quantidade,
                ClienteId = 1, // Verifique se ClienteId é válido
                Total = valorTotal,
                Transacao_Data = DateTime.Now,
                Imposto = imposto,
                Desconto = desconto,
                Add_Por = usuarioLogado
            };

            if (transacao == null)
            {
                TempData["MensagemErro"] = "Falha ao criar objeto de transação.";
                return RedirectToAction("Index");
            }

            var sucesso = _vendaRepositorio.Vender(new VendasModel
            {
                ProdutoId = produtoId,
                Quantidade = quantidade,
                ValorTotal = valorTotal,
                DataVenda = DateTime.Now
            });

            if (sucesso)
            {
                produto.Quantidade -= quantidade;
                _produtoRepositorio.Atualizar(produto);
                _vendaRepositorio.RegistrarTransacao(transacao);
                _vendaRepositorio.GerarComprovanteVenda(new VendasModel
                {
                    ProdutoId = produtoId,
                    Quantidade = quantidade,
                    ValorTotal = valorTotal,
                    DataVenda = DateTime.Now
                }, produto, transacao);

                TempData["MensagemSucesso"] = "Venda realizada com sucesso!";
            }
            else
            {
                TempData["MensagemErro"] = "Erro ao registrar venda.";
            }

            return RedirectToAction("Index");
        }
    }
}
