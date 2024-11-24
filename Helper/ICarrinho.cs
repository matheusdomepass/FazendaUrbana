using FazendaUrbana.Models;

namespace FazendaUrbana.Helper
{
    public interface ICarrinho
    {
        void AdicionarProduto(VendasModel vendas);
        void RemoverProduto(int produtoID);
        void AtualizarQuantidade(int produtoID, int quantidade);
        List<VendasModel> ObterCarrinho();
        void LimparCarrinho();
        void SalvarCarrinho(List<VendasModel> carrinho);
    }
}
