namespace FazendaUrbana.Models
{
    public class VendasModel
    {
        public List<ProdutoModel> Produtos {  get; set; }
        public List<VendasModel> Carrinho { get; set; }
        public ProdutoModel Produto { get; set; }
        public int Id { get; set; }
        public string NomeProduto { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataVenda { get; set; } = DateTime.Now;
        public string Add_Por {  get; set; }
        public string NomeCliente { get; set; }
        public int TransacaoId { get; set; }
        public TransacaoModel Transacao { get; set; }
    }
}
