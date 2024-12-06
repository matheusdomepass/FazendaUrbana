using FazendaUrbana.Data;
using FazendaUrbana.Models;
using Microsoft.EntityFrameworkCore;

namespace FazendaUrbana.Repositorio
{
    public class FornecedorRepositorio : IFornecedorRepositorio
    {
        private readonly BancoContext _bancoContext;
        public FornecedorRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public FornecedorModel ListarPorId(int id)
        {
            return _bancoContext.Fornecedores.Include(c => c.Endereco).FirstOrDefault(x => x.Id == id);
        }
        public List<FornecedorModel> BuscarTodos()
        {
            return _bancoContext.Fornecedores.Include(c => c.Endereco).ToList();
        }

        public FornecedorModel Adicionar(FornecedorModel fornecedor)
        {
            // GRAVAR NO BANCO DE DADOS
            _bancoContext.Fornecedores.Add(fornecedor);
            _bancoContext.SaveChanges();

            return fornecedor;
        }

        public FornecedorModel Atualizar(FornecedorModel fornecedor)
        {
            FornecedorModel fornecedorDB = ListarPorId(fornecedor.Id);

            if (fornecedorDB == null)
            {
                throw new Exception("Houve um erro na atualização do fornecedor");
            }

            fornecedorDB.Nome = fornecedor.Nome;
            fornecedorDB.Email = fornecedor.Email;
            fornecedorDB.Celular = fornecedor.Celular;
            fornecedorDB.CNPJ = fornecedor.CNPJ;
            if (fornecedorDB.Endereco != null && fornecedor.Endereco != null)
            {
                fornecedorDB.Endereco.Rua = fornecedor.Endereco.Rua;
                fornecedorDB.Endereco.Numero = fornecedor.Endereco.Numero;
                fornecedorDB.Endereco.Complemento = fornecedor.Endereco.Complemento;
                fornecedorDB.Endereco.Bairro = fornecedor.Endereco.Bairro;
                fornecedorDB.Endereco.Cidade = fornecedor.Endereco.Cidade;
                fornecedorDB.Endereco.Estado = fornecedor.Endereco.Estado;
                fornecedorDB.Endereco.CEP = fornecedor.Endereco.CEP;
            }

            _bancoContext.Fornecedores.Update(fornecedorDB);
            _bancoContext.SaveChanges();

            return fornecedorDB;
        }

        public bool Apagar(int id)
        {
            FornecedorModel fornecedorDB = ListarPorId(id);

            if (fornecedorDB == null)
            {
                throw new Exception("Houve um erro ao apagar o fornecedor");
            }

            _bancoContext.Fornecedores.Remove(fornecedorDB);
            _bancoContext.SaveChanges();

            return true;
        }

        public FornecedorModel ListarPorCNPJ(string cnpj)
        {
            return _bancoContext.Fornecedores.FirstOrDefault(x => x.CNPJ == cnpj);
        }
    }
}
