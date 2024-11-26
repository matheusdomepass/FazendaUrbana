namespace FazendaUrbana.Models
{
    public class TransacaoModel
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public int ClienteId { get; set; }
        public decimal Total { get; set; }
        public DateTime Transacao_Data { get; set; } = DateTime.Now;
        public List<VendasModel> Vendas { get; set; } = new List<VendasModel>();
    }
}
