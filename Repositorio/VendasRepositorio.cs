using FazendaUrbana.Data;
using FazendaUrbana.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public void RegistrarTransacao(TransacaoModel transacao)
        {
            _bancoContext.Transacoes.Add(transacao);
            _bancoContext.SaveChanges();
        }

        public List<VendasModel> BuscarTodos()
        {
            return _bancoContext.Vendas.Include(v => v.Produto).ToList();
        }
        public byte[] GerarComprovanteVenda(VendasModel venda, ProdutoModel produto, TransacaoModel transacao)
        {
            using (var ms = new MemoryStream())
            {
                var document = new iTextSharp.text.Document();
                PdfWriter.GetInstance(document, ms);
                document.Open();

                document.Add(new Paragraph("Comprovante de Venda"));
                document.Add(new Paragraph($"Data da Venda: {venda.DataVenda}"));
                document.Add(new Paragraph($"Nome do Cliente: {venda.NomeCliente}"));
                document.Add(new Paragraph($"Produto: {produto.Nome}"));
                document.Add(new Paragraph($"Quantidade Vendida: {venda.Quantidade}"));
                document.Add(new Paragraph($"Valor Unitário: {produto.Valor:C}"));
                document.Add(new Paragraph($"Valor Total: {venda.ValorTotal:C}"));
                document.Add(new Paragraph($"Vendido Por: {transacao.Add_Por}"));

                document.Close();

                return ms.ToArray();
            }
        }

        public bool Vender(VendasModel vendas, ProdutoModel produto, TransacaoModel transacao)
        {
            if (produto == null || produto.Quantidade < vendas.Quantidade)
            {
                return false;
            }

            produto.Quantidade -= vendas.Quantidade;
            _produtorepositorio.Atualizar(produto);

            _bancoContext.Vendas.Add(vendas);
            _bancoContext.Transacoes.Add(transacao);
            _bancoContext.SaveChanges();

            return true;
        }

        public ProdutoModel ListarProdutoPorId(int id)
        {
            return _bancoContext.Produtos.FirstOrDefault(p => p.Id == id);
        }
    }
}
