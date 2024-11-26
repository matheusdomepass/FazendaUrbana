using FazendaUrbana.Models;

namespace FazendaUrbana.Repositorio
{
    public interface IVendaRepositorio
    {
        VendasModel ListarPorId(int id);
        bool Vender(VendasModel vendas, ProdutoModel produto, TransacaoModel transacao);
        ProdutoModel ListarProdutoPorId(int id);
        void RegistrarTransacao(TransacaoModel transacao);
        List<VendasModel> BuscarTodos(bool agrupada = false);
        byte[] GerarComprovanteVenda(TransacaoModel transacao, List<VendasModel> vendas);
    }
}
