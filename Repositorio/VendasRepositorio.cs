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
            return _bancoContext.Vendas.ToList();
        }
        public bool Vender(VendasModel vendas, ProdutoModel produto, TransacaoModel transacao)
        {
            if (produto == null || produto.Quantidade < vendas.Quantidade)
            {
                return false;
            }

            produto.Quantidade -= vendas.Quantidade;
            _produtorepositorio.Atualizar(produto);

            transacao.Id = 0;

            _bancoContext.Transacoes.Add(transacao);
            _bancoContext.Vendas.Add(vendas);
            _bancoContext.SaveChanges();

            return true;
        }

        public ProdutoModel ListarProdutoPorId(int id)
        {
            return _bancoContext.Produtos.FirstOrDefault(p => p.Id == id);
        }

        public byte[] GerarComprovanteVenda(TransacaoModel transacao, List<VendasModel> vendas)
        {
            using (var ms = new MemoryStream())
            {
                var document = new iTextSharp.text.Document();
                PdfWriter.GetInstance(document, ms);
                document.Open();

                document.Add(new Paragraph("Fazenda Boa Vista"));
                document.Add(new Paragraph(" "));

                document.Add(new Paragraph("Comprovante de Venda"));
                document.Add(new Paragraph($"Data da Venda: {transacao.Transacao_Data}"));
                document.Add(new Paragraph($"Nome do Cliente: {vendas.FirstOrDefault()?.NomeCliente}"));                
                document.Add(new Paragraph(" "));

                document.Add(new Paragraph($"Produtos: "));
                foreach (var venda in vendas)
                {
                    document.Add(new Paragraph($"- Produto: {venda.NomeProduto}"));
                    document.Add(new Paragraph($"  Quantidade Vendida: {venda.Quantidade}"));
                    document.Add(new Paragraph($"  Valor Unitário: {venda.ValorUnitario:C}"));
                    document.Add(new Paragraph($"  Valor Total: {venda.ValorTotal:C}"));
                    document.Add(new Paragraph(" "));
                }

                document.Add(new Paragraph($"Quantidade Total de Produtos: {vendas.Sum(v => v.Quantidade)}"));
                document.Add(new Paragraph($"Valor Total da Transação: {transacao.Total:C}"));
                document.Add(new Paragraph($"Vendido Por: {vendas[0].Add_Por}"));

                document.Close();

                return ms.ToArray();
            }
        }

        public TransacaoModel ListarTransacaoPorId(int id)
        {
            return _bancoContext.Transacoes
                .Include(t => t.Vendas)
                .FirstOrDefault(t => t.Id == id);
        }
        public List<VendasModel> ListarVendasPorTransacaoId()
        {
            return _bancoContext.Vendas
                .Include(v => v.Transacao)
                .ToList();
        }

    }
}
