using FazendaUrbana.Models;

namespace FazendaUrbana.Repositorio
{
    public interface IClienteRepositorio
    {
        ClienteModel ListarPorId(int id);
        List<ClienteModel> BuscarTodos();
        ClienteModel ListarPorCPF_CNPJ(string cpf_cnpj);
        ClienteModel Adicionar(ClienteModel cliente);
        ClienteModel Atualizar(ClienteModel cliente);
        bool Apagar(int id);

    }
}
