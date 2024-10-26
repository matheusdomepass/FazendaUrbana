using FazendaUrbana.Filters;
using FazendaUrbana.Models;
using FazendaUrbana.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace FazendaUrbana.Controllers
{
    [PaginaUsuarioLogado]
    public class ClienteController : Controller
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        public ClienteController(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }
        public IActionResult Index()
        {
            List<ClienteModel> clientes = _clienteRepositorio.BuscarTodos();
            return View(clientes);
        }
        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            ClienteModel cliente = _clienteRepositorio.ListarPorId(id);
            return View(cliente);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            ClienteModel cliente = _clienteRepositorio.ListarPorId(id);
            return View(cliente);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _clienteRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Cliente apagado com sucesso";
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, não conseguimos apagar o cliente";
                }
                return RedirectToAction("Index");

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar o cliente, mais detalhes do erro {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(ClienteModel cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _clienteRepositorio.Adicionar(cliente);
                    TempData["MensagemSucesso"] = "Cliente cadastrado com sucesso";
                    return RedirectToAction("Index");
                }


                return View(cliente);

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o cliente, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public IActionResult Alterar(ClienteModel cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var clienteExistente = _clienteRepositorio.ListarPorId(cliente.Id);

                    if (clienteExistente == null)
                    {
                        TempData["MensagemErro"] = "Cliente não encontrado";
                        return RedirectToAction("Index");
                    }
                    clienteExistente.Nome = cliente.Nome;
                    clienteExistente.Email = cliente.Email;
                    clienteExistente.Celular = cliente.Celular;
                    clienteExistente.CPF_CNPJ = cliente.CPF_CNPJ;
                    clienteExistente.Endereco.Rua = cliente.Endereco.Rua;
                    clienteExistente.Endereco.Numero = cliente.Endereco.Numero;
                    clienteExistente.Endereco.Complemento = cliente.Endereco.Complemento;
                    clienteExistente.Endereco.Bairro = cliente.Endereco.Bairro;
                    clienteExistente.Endereco.Cidade = cliente.Endereco.Cidade;
                    clienteExistente.Endereco.Estado = cliente.Endereco.Estado;
                    clienteExistente.Endereco.CEP = cliente.Endereco.CEP;

                    _clienteRepositorio.Atualizar(clienteExistente);
                    _clienteRepositorio.Atualizar(cliente);

                    TempData["MensagemSucesso"] = "Cliente alterado com sucesso";
                    return RedirectToAction("Index");
                }
                return View("Editar", cliente);

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos alterar o cliente, tente novamente, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
