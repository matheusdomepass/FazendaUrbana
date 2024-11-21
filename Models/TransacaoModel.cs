namespace FazendaUrbana.Models
{
    public class TransacaoModel
    {
        public int Id { get; set; }
        public string Tipo { get; set; } 
        public int Quantidade { get; set; }
        public int ClienteId { get; set; }
        public decimal Total { get; set; }
        public DateTime Transacao_Data { get; set; } = DateTime.Now;
        public decimal Imposto { get; set; }
        public decimal Desconto { get; set; }
        public string Add_Por { get; set; }
    }
}
