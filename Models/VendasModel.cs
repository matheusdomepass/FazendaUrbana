namespace FazendaUrbana.Models
{
    public class VendasModel
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataVenda { get; set; } = DateTime.Now;
    }
}
