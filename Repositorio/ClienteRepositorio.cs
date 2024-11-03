using FazendaUrbana.Data;
using FazendaUrbana.Models;
using Microsoft.EntityFrameworkCore;

namespace FazendaUrbana.Repositorio
{
    public class ClienteRepositorio : IClienteRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ClienteRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }
        public ClienteModel ListarPorId(int id)
        {
            return _bancoContext.Clientes.Include(c => c.Endereco).FirstOrDefault(x => x.Id == id);
        }
        public List<ClienteModel> BuscarTodos()
        {
            return _bancoContext.Clientes.Include(c => c.Endereco).ToList();
        }

        public ClienteModel Adicionar(ClienteModel cliente)
        {
            // GRAVAR NO BANCO DE DADOS
            _bancoContext.Clientes.Add(cliente);
            _bancoContext.SaveChanges();

            return cliente;
        }

        public ClienteModel Atualizar(ClienteModel cliente)
        {
            ClienteModel clienteDB = ListarPorId(cliente.Id);

            if (clienteDB == null)
            {
                throw new Exception("Houve um erro na atualização do contato");
            }

            clienteDB.Nome = cliente.Nome;
            clienteDB.Email = cliente.Email;
            clienteDB.Celular = cliente.Celular;
            clienteDB.CPF_CNPJ = cliente.CPF_CNPJ;
            if (clienteDB.Endereco != null && cliente.Endereco != null)
            {
                clienteDB.Endereco.Rua = cliente.Endereco.Rua;
                clienteDB.Endereco.Numero = cliente.Endereco.Numero;
                clienteDB.Endereco.Complemento = cliente.Endereco.Complemento;
                clienteDB.Endereco.Bairro = cliente.Endereco.Bairro;
                clienteDB.Endereco.Cidade = cliente.Endereco.Cidade;
                clienteDB.Endereco.Estado = cliente.Endereco.Estado;
                clienteDB.Endereco.CEP = cliente.Endereco.CEP;
            }

            _bancoContext.Clientes.Update(clienteDB);
            _bancoContext.SaveChanges();

            return clienteDB;
        }

        public bool Apagar(int id)
        {
            ClienteModel clienteDB = ListarPorId(id);

            if (clienteDB == null)
            {
                throw new Exception("Houve um erro ao apagar o contato");
            }

            _bancoContext.Clientes.Remove(clienteDB);
            _bancoContext.SaveChanges();

            return true;
        }
    }
}
