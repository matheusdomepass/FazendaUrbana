using FazendaUrbana.Helper;
using FazendaUrbana.Models;
using FazendaUrbana.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FazendaUrbana.Controllers
{
    public class VendasController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly IVendaRepositorio _vendaRepositorio;
        private readonly ICarrinho _carrinho;
        private readonly IHttpContextAccessor _contextAccessor;

        public VendasController(IProdutoRepositorio produtoRepositorio, IVendaRepositorio vendaRepositorio, ICarrinho carrinho, IHttpContextAccessor contextAccessor)
        {
            _carrinho = carrinho;
            _produtoRepositorio = produtoRepositorio;
            _vendaRepositorio = vendaRepositorio;
            _contextAccessor = contextAccessor;
        }
        public IActionResult Index(int id)
        {
            var vendas = _vendaRepositorio.ListarVendasPorTransacaoId(id);

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
            var carrinho = _carrinho.ObterCarrinho();

            var model = new VendasModel {
                Produtos = produtos,
                Carrinho = carrinho
            };

            return View(model);
        }
        
    
        public IActionResult AdicionarAoCarrinho(int produtoId, int quantidade, string nomeCliente, string add_Por)
        {
            if (quantidade <= 0)
            {
                TempData["MensagemErro"] = "Quantidade solicitada não pode ser igual ou menor que 0.";
                return RedirectToAction("Vender");
            }

            var produto = _vendaRepositorio.ListarProdutoPorId(produtoId);
            if (produto == null)
            {
                TempData["MensagemErro"] = "Produto não encontrado!";
                return RedirectToAction("Vender");
            }

            if (produto.Quantidade < quantidade)
            {
                TempData["MensagemErro"] = "Quantidade solicitada excede o estoque disponível.";
                return RedirectToAction("Vender");
            }
            var carrinho = _carrinho.ObterCarrinho();
            var itemExistente = carrinho.FirstOrDefault(c => c.ProdutoId == produtoId);
            if (itemExistente != null)
            {
                itemExistente.Quantidade += quantidade;
                itemExistente.ValorTotal = itemExistente.Quantidade * produto.Valor;
            }
            else
            {
                carrinho.Add(new VendasModel
                {
                    ProdutoId = produtoId,
                    NomeProduto = produto.Nome,
                    Quantidade = quantidade,
                    ValorUnitario = produto.Valor,
                    ValorTotal = quantidade * produto.Valor,
                    Add_Por = add_Por,
                    NomeCliente = nomeCliente,
                });
            }

            _carrinho.SalvarCarrinho(carrinho);

            TempData["MensagemSucesso"] = "Produto adicionado ao carrinho!";
            return RedirectToAction("Vender");
        }

        [HttpPost]
        public IActionResult FinalizarVenda(int produtoId, int quantidade, string nomeCliente, string add_Por)
        {
            var carrinho = _carrinho.ObterCarrinho();
            if (carrinho == null || !carrinho.Any())
            {
                TempData["MensagemErro"] = "O carrinho está vazio!";
                return RedirectToAction("Vender");
            }
            string usuarioJson = _contextAccessor.HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(usuarioJson))
            {
                TempData["MensagemErro"] = "Usuário não está logado";
                return RedirectToAction("Index");
            }

            var usuario = JsonConvert.DeserializeObject<UsuarioModel>(usuarioJson);

            var transacao = new TransacaoModel
            {
                Quantidade = carrinho.Sum(c => c.Quantidade),
                ClienteId = 1,
                Total = carrinho.Sum(c => c.ValorTotal),
                Transacao_Data = DateTime.Now
            };


            var vendas = new List<VendasModel>();

            foreach (var item in carrinho)
            {
                var produto = _vendaRepositorio.ListarProdutoPorId(item.ProdutoId);
                if (produto == null || produto.Quantidade < item.Quantidade)
                {
                    TempData["MensagemErro"] = $"Produto '{item.NomeProduto}' não tem estoque suficiente.";
                }

                produto.Quantidade -= item.Quantidade;
                _produtoRepositorio.Atualizar(produto);

                var venda = new VendasModel
                {
                    ProdutoId = item.ProdutoId,
                    Quantidade = item.Quantidade,
                    ValorUnitario = produto.Valor,
                    ValorTotal = item.ValorTotal,
                    DataVenda = DateTime.Now,
                    Add_Por = usuario.Nome,
                    NomeCliente = item.NomeCliente,
                    TransacaoId = transacao.Id,
                    NomeProduto = item.NomeProduto
                };

                var sucesso = _vendaRepositorio.Vender(venda, produto, transacao);
                if (!sucesso)
                {
                    TempData["MensagemErro"] = $"Erro ao registrar venda para o produto '{item.NomeProduto}'.";
                    return RedirectToAction("Vender");
                }
                vendas.Add(venda);

            }
            var comprovante = _vendaRepositorio.GerarComprovanteVenda(transacao, vendas);           
            _carrinho.LimparCarrinho();

            TempData["MensagemSucesso"] = "Venda finalizada com sucesso!";
            return File(comprovante, "application/pdf", "ComprovanteVenda.pdf");
        }
        [HttpPost]
        public IActionResult AtualizarCarrinho(int produtoId, int novaQuantidade)
        {
            if (novaQuantidade <= 0)
            {
                TempData["MensagemErro"] = "A quantidade deve ser maior que zero.";
                return RedirectToAction("Vender");
            }

            var carrinho = _carrinho.ObterCarrinho();

            var item = carrinho.FirstOrDefault(c => c.ProdutoId == produtoId);
            if (item != null)
            {
                var produto = _produtoRepositorio.ListarPorId(produtoId);
                if (produto == null || produto.Quantidade < novaQuantidade)
                {
                    TempData["MensagemErro"] = "Quantidade solicitada excede o estoque disponível.";
                    return RedirectToAction("Vender");
                }

                item.Quantidade = novaQuantidade;
                item.ValorTotal = item.Quantidade * item.ValorUnitario;

                _carrinho.SalvarCarrinho(carrinho);

                TempData["MensagemSucesso"] = "Carrinho atualizado!";
            }
            else
            {
                TempData["MensagemErro"] = "Produto não encontrado no carrinho.";
            }

            return RedirectToAction("Vender");
        }
        [HttpPost]
        public IActionResult RemoverDoCarrinho(int produtoId)
        {
            var carrinho = _carrinho.ObterCarrinho();

            var item = carrinho.FirstOrDefault(c => c.ProdutoId == produtoId);
            if (item != null)
            {
                carrinho.Remove(item);
                _carrinho.SalvarCarrinho(carrinho);

                TempData["MensagemSucesso"] = "Produto removido do carrinho!";
            }
            else
            {
                TempData["MensagemErro"] = "Produto não encontrado no carrinho.";
            }

            return RedirectToAction("Vender");
        }
    }
}
