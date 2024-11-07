using FazendaUrbana.Data;
using FazendaUrbana.Models;
using Microsoft.AspNetCore.Mvc;

namespace FazendaUrbana.Repositorio
{
    public class VendasRepositorio : IVendaRepositorio
    {
        private readonly BancoContext _bancoContext;
        private readonly IProdutoRepositorio _produtorepositorio;

        public VendasRepositorio(BancoContext bancoContext, IProdutoRepositorio produtorepositorio)
        {
            _bancoContext = bancoContext;
            _produtorepositorio = produtorepositorio;
        }

        public VendasModel ListarPorId(int id)
        {
            return _bancoContext.Vendas.FirstOrDefault(x => x.Id == id);
        }

        public bool Vender(VendasModel vendas)
        {
            var produto = _produtorepositorio.ListarPorId(vendas.Id);
            if (produto == null || produto.Quantidade < vendas.Quantidade)
            {
                return false;
            }

            produto.Quantidade -= vendas.Quantidade;
            _produtorepositorio.Atualizar(produto);

            _bancoContext.Vendas.Add(vendas);
            _bancoContext.SaveChanges();

            return true;
        }
        public void RegistrarTransacao(TransacaoModel transacao)
        {
            _bancoContext.Transacoes.Add(transacao);
            _bancoContext.SaveChanges();
        }

        public List<VendasModel> BuscarTodos()
        {
            return _bancoContext.Vendas.ToList();
        }
    }
}
