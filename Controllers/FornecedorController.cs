using FazendaUrbana.Filters;
using FazendaUrbana.Models;
using FazendaUrbana.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace FazendaUrbana.Controllers
{
    [PaginaUsuarioLogado]
    public class FornecedorController : Controller
    {
        private readonly IFornecedorRepositorio _fornecedorRepositorio;
        private readonly IHttpContextAccessor _contextAccessor;
        public FornecedorController(IFornecedorRepositorio fornecedorRepositorio, IHttpContextAccessor contextAccessor)
        {
            _fornecedorRepositorio = fornecedorRepositorio;
            _contextAccessor = contextAccessor;
        }
        public IActionResult Index()
        {
            List<FornecedorModel> fornecedores = _fornecedorRepositorio.BuscarTodos();
            return View(fornecedores);
        }
        public IActionResult Criar()
        {
            return View();
        }
        public IActionResult Editar(int id)
        {
            FornecedorModel fornecedor = _fornecedorRepositorio.ListarPorId(id);
            return View(fornecedor);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            FornecedorModel fornecedor = _fornecedorRepositorio.ListarPorId(id);
            return View(fornecedor);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _fornecedorRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Fornecedor apagado com sucesso";
                }
                else
                {
                    TempData["MensagemErro"] = "Ops, não conseguimos apagar o fornecedor";
                }
                return RedirectToAction("Index");

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar o fornecedor, mais detalhes do erro {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Criar(FornecedorModel fornecedor)
        {
            try
            {
                string usuarioJson = _contextAccessor.HttpContext.Session.GetString("sessaoUsuarioLogado");

                if (string.IsNullOrEmpty(usuarioJson))
                {
                    TempData["MensagemErro"] = "Usuário não está logado";
                    return RedirectToAction("Index");
                }

                var usuario = JsonConvert.DeserializeObject<UsuarioModel>(usuarioJson);

                fornecedor.Add_Por = usuario.Nome;
                var fornecedorExistente = _fornecedorRepositorio.ListarPorCNPJ(fornecedor.CNPJ);
                if (fornecedorExistente != null)
                {
                    TempData["MensagemErro"] = "Já existe um fornecedor com este documento!";
                    return View(fornecedor);
                }
                _fornecedorRepositorio.Adicionar(fornecedor);
                TempData["MensagemSucesso"] = "Fornecedor cadastrado com sucesso";
                return RedirectToAction("Index");

            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o fornecedor, tente novamente, detalhe do erro: Campos não preenchidos corretamente ";
                return RedirectToAction("Criar", fornecedor);
            }

        }
        [HttpPost]
        public IActionResult Alterar(FornecedorModel fornecedor)
        {
            try
            {
                var fornecedorExistente = _fornecedorRepositorio.ListarPorId(fornecedor.Id);

                if (fornecedorExistente == null)
                {
                    TempData["MensagemErro"] = "Fornecedor não encontrado";
                    return RedirectToAction("Index");
                }

                var fornecedorDuplicado = _fornecedorRepositorio.ListarPorCNPJ(fornecedor.CNPJ);
                if (fornecedorDuplicado != null && fornecedorDuplicado.Id != fornecedor.Id)
                {
                    TempData["MensagemErro"] = "Já existe um fornecedor cadastrado com este CNPJ.";
                    return View("Editar", fornecedor);
                }
                string usuarioJson = _contextAccessor.HttpContext.Session.GetString("sessaoUsuarioLogado");
                if (string.IsNullOrEmpty(usuarioJson))
                {
                    TempData["MensagemErro"] = "Usuário não está logado.";
                    return RedirectToAction("Index");
                }

                var usuario = JsonConvert.DeserializeObject<UsuarioModel>(usuarioJson);

                fornecedorExistente.Nome = fornecedor.Nome;
                fornecedorExistente.Email = fornecedor.Email;
                fornecedorExistente.Celular = fornecedor.Celular;
                fornecedorExistente.CNPJ = fornecedor.CNPJ;
                fornecedorExistente.Endereco.Rua = fornecedor.Endereco.Rua;
                fornecedorExistente.Endereco.Numero = fornecedor.Endereco.Numero;
                fornecedorExistente.Endereco.Complemento = fornecedor.Endereco.Complemento;
                fornecedorExistente.Endereco.Bairro = fornecedor.Endereco.Bairro;
                fornecedorExistente.Endereco.Cidade = fornecedor.Endereco.Cidade;
                fornecedorExistente.Endereco.Estado = fornecedor.Endereco.Estado;
                fornecedorExistente.Endereco.CEP = fornecedor.Endereco.CEP;

                _fornecedorRepositorio.Atualizar(fornecedorExistente);
                _fornecedorRepositorio.Atualizar(fornecedor);

                TempData["MensagemSucesso"] = "Fornecedor alterado com sucesso";
                return RedirectToAction("Index");


            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos alterar o fornecedor, tente novamente, detalhe do erro: Campos não preenchidos corretamente ";
                return RedirectToAction("Editar", fornecedor);
            }
        }
    }
}
