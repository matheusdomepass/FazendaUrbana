using ControleContratos.Models;

namespace ControleContratos.Repositorio
{
    public interface IContatoRepositorio
    {
        List<ContatoModel> BuscarContatos();
        ContatoModel Adicionar(ContatoModel contato);

    }
}
