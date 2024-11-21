using FazendaUrbana.Models;

namespace FazendaUrbana.Repositorio
{
    public interface IVendaRepositorio
    {
        VendasModel ListarPorId(int id);
        bool Vender(VendasModel vendas);
        void RegistrarTransacao(TransacaoModel transacao);
        List<VendasModel> BuscarTodos();
        string GerarComprovanteVenda(VendasModel venda, ProdutoModel produto, TransacaoModel transacao);
    }
}
