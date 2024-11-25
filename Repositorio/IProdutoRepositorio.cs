using FazendaUrbana.Models;

namespace FazendaUrbana.Repositorio
{
    public interface IProdutoRepositorio
    {
        ProdutoModel ListarPorId(int id);
        ProdutoModel ListarPorNome(string nome);
        List<ProdutoModel> BuscarTodos();
        ProdutoModel Adicionar(ProdutoModel produto);
        ProdutoModel Atualizar(ProdutoModel produto);
        bool Apagar(int id);
    }
}
