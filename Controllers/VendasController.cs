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
            var produtos = _produtoRepositorio.BuscarTodos();

            if(produtos == null || !produtos.Any())
            {
                TempData["MensagemErro"] = "Nenhum produto encontrado";
                return RedirectToAction("Index");
            }
            var model = new VendasModel { Produtos = produtos };

            return View(model);
        }

        [HttpPost]
        public IActionResult Vender(int produtoId, int quantidade, string nomeCliente, string add_Por, TransacaoModel transacaoVenda)
        {            
            var produto = _vendaRepositorio.ListarProdutoPorId(produtoId);
            if (produto == null)
            {
                TempData["MensagemErro"] = "Produto não encontrado!";
                return RedirectToAction("Index");
            }
            if (produto.Quantidade < quantidade)
            {
                TempData["MensagemErro"] = "Quantidade solicitada excede o estoque disponível.";
                return RedirectToAction("Index");
            }
            if (quantidade == 0)
            {
                TempData["MensagemErro"] = "Quantidade solicitada não pode ser igual a 0.";
                return RedirectToAction("Index");
            }
            var valorTotal = produto.Valor * quantidade;
            
            var venda = new VendasModel
            {                
                ProdutoId = produtoId,
                Quantidade = quantidade,
                ValorTotal = valorTotal,
                DataVenda = DateTime.Now,
                Add_Por = add_Por,
                NomeCliente = nomeCliente,
                Transacao = transacaoVenda
            };
            var transacao = new TransacaoModel
            {
                Quantidade = quantidade,
                ClienteId = 1,
                Total = valorTotal,
                Transacao_Data = DateTime.Now,
                Add_Por = add_Por
            };
            if (string.IsNullOrEmpty(venda.Transacao.Add_Por))
            {
                venda.Transacao.Add_Por = "Usuário Desconhecido";
            }

            if (venda.Transacao == null)
            {
                TempData["MensagemErro"] = "Falha ao criar objeto de transação.";
                return RedirectToAction("Index");
            }

            var sucesso = _vendaRepositorio.Vender(venda, produto, transacao);

            if (sucesso)
            {
                produto.Quantidade -= quantidade;
                _produtoRepositorio.Atualizar(produto);
                TempData["MensagemSucesso"] = "Venda realizada com sucesso!";
                var comprovante = _vendaRepositorio.GerarComprovanteVenda(new VendasModel
                {
                    ProdutoId = produtoId,
                    Quantidade = quantidade,
                    ValorTotal = valorTotal,
                    DataVenda = DateTime.Now,
                    NomeCliente = nomeCliente
                }, produto, transacao);

                if(comprovante != null)
                {
                    TempData["MensagemSucesso"] = "Gerando comprovante...";
                    return File(comprovante, "application/pdf", "ComprovanteVenda.pdf");
                }
            }
            else
            {
                TempData["MensagemErro"] = "Erro ao registrar venda.";
            }

            return RedirectToAction("Index");
        }
    }
}
