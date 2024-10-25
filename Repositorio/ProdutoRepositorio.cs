using FazendaUrbana.Data;
using FazendaUrbana.Models;

namespace FazendaUrbana.Repositorio
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ProdutoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public ProdutoModel ListarPorId(int id)
        {
            return _bancoContext.Produtos.FirstOrDefault(x => x.Id == id);
        }
        public List<ProdutoModel> BuscarTodos()
        {
            return _bancoContext.Produtos.ToList();
        }

        public ProdutoModel Adicionar(ProdutoModel produto)
        {
            // GRAVAR NO BANCO DE DADOS
            _bancoContext.Produtos.Add(produto);
            _bancoContext.SaveChanges();

            return produto;
        }

        public ProdutoModel Atualizar(ProdutoModel produto)
        {
            ProdutoModel contatoDB = ListarPorId(produto.Id);

            if (contatoDB == null)
            {
                throw new Exception("Houve um erro na atualização do produto");
            }

            contatoDB.Nome = produto.Nome;
            contatoDB.Descricao = produto.Descricao;
            contatoDB.Valor = produto.Valor;
            contatoDB.Categoria = produto.Categoria;
            contatoDB.Quantidade = produto.Quantidade;
            contatoDB.Add_Por = produto.Add_Por;
            contatoDB.Add_Data = produto.Add_Data;

            _bancoContext.Produtos.Update(contatoDB);
            _bancoContext.SaveChanges();

            return contatoDB;
        }

        public bool Apagar(int id)
        {
            ProdutoModel contatoDB = ListarPorId(id);

            if (contatoDB == null)
            {
                throw new Exception("Houve um erro ao apagar o produto");
            }

            _bancoContext.Produtos.Remove(contatoDB);
            _bancoContext.SaveChanges();

            return true;
        }
    }
}

