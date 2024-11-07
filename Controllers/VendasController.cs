using FazendaUrbana.Models;
using FazendaUrbana.Repositorio;
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
            var produtos = _produtoRepositorio.BuscarTodos();
            return View(produtos);
        }

        public IActionResult Vender(int produtoId, int quantidade, decimal imposto, decimal desconto)
        {
            var produto = _produtoRepositorio.ListarPorId(produtoId);
            if (produto == null || produto.Quantidade < quantidade)
            {
                TempData["MensagemErro"] = "Estoque insuficiente!";
                return RedirectToAction("Index");
            }

            var valorTotal = produto.Valor * quantidade;
            valorTotal -= desconto;
            valorTotal += valorTotal * (imposto / 100);

            var venda = new VendasModel
            {
                ProdutoId = produtoId,
                Quantidade = quantidade,
                ValorTotal = valorTotal,
                DataVenda = DateTime.Now
            };
            var transacao = new TransacaoModel
            {
                Tipo = "Venda",
                Quantidade = quantidade,
                ClienteId = 1,
                Total = valorTotal,
                Transacao_Data = DateTime.Now,
                Imposto = imposto,
                Desconto = desconto,
                Add_Por = 1
            };

            var sucesso = _vendaRepositorio.Vender(venda);

            if (sucesso)
            {
                _vendaRepositorio.RegistrarTransacao(transacao);

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
