namespace FazendaUrbana.Models
{
    public class SmtpModel
    {
        public string UserName { get; set; }
        public string Nome { get; set; }
        public string Host { get; set; }
        public string Senha { get; set; }
        public int Porta { get; set; }
        public bool EnableSSL { get; set; }
    }

}
