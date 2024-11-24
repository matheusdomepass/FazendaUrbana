using FazendaUrbana.Helper;
using FazendaUrbana.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

public class Carrinho : ICarrinho
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public Carrinho(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private List<VendasModel> ObterCarrinhoInterno()
    {
        var sessao = _httpContextAccessor.HttpContext.Session.GetString("Carrinho");
        return string.IsNullOrEmpty(sessao) ? new List<VendasModel>() : JsonConvert.DeserializeObject<List<VendasModel>>(sessao);
    }

    public void AdicionarProduto(VendasModel produto)
    {
        var carrinho = ObterCarrinhoInterno();
        var itemExistente = carrinho.FirstOrDefault(c => c.ProdutoId == produto.ProdutoId);

        if (itemExistente != null)
        {
            itemExistente.Quantidade += produto.Quantidade;
            itemExistente.ValorTotal = itemExistente.Quantidade * produto.ValorUnitario;
        }
        else
        {
            carrinho.Add(produto);
        }

        SalvarCarrinho(carrinho);
    }

    public void RemoverProduto(int produtoId)
    {
        var carrinho = ObterCarrinhoInterno();
        var item = carrinho.FirstOrDefault(c => c.ProdutoId == produtoId);

        if (item != null)
        {
            carrinho.Remove(item);
            SalvarCarrinho(carrinho);
        }
    }

    public void AtualizarQuantidade(int produtoId, int novaQuantidade)
    {
        var carrinho = ObterCarrinhoInterno();
        var item = carrinho.FirstOrDefault(c => c.ProdutoId == produtoId);

        if (item != null)
        {
            item.Quantidade = novaQuantidade;
            item.ValorTotal = novaQuantidade * item.ValorUnitario;
            SalvarCarrinho(carrinho);
        }
    }

    public List<VendasModel> ObterCarrinho()
    {
        return ObterCarrinhoInterno();
    }

    public void LimparCarrinho()
    {
        _httpContextAccessor.HttpContext.Session.Remove("Carrinho");
    }

    public void SalvarCarrinho(List<VendasModel> carrinho)
    {
        var session = _httpContextAccessor.HttpContext.Session;
        var carrinhoJson = JsonConvert.SerializeObject(carrinho);
        _httpContextAccessor.HttpContext.Session.SetString("Carrinho", carrinhoJson);
    }
}
