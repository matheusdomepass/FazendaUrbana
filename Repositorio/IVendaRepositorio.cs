using FazendaUrbana.Models;

namespace FazendaUrbana.Repositorio
{
    public interface IVendaRepositorio
    {
        VendasModel ListarPorId(int id);
        bool Vender(VendasModel vendas);
        void RegistrarTransacao(TransacaoModel transacao);
    }
}
