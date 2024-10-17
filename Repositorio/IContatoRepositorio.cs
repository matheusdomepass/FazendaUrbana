using FazendaUrbana.Models;

namespace FazendaUrbana.Repositorio
{
    public interface IContatoRepositorio
    {
        ContatoModel ListarPorId(int id);
        List<ContatoModel> BuscarContatos();
        ContatoModel Adicionar(ContatoModel contato);
        ContatoModel Atualizar(ContatoModel contato);
        bool Apagar(int id);

    }
}
