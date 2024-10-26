using System.ComponentModel.DataAnnotations;

namespace FazendaUrbana.Models
{
    public class ClienteModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome do cliente")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite o e-mail do cliente")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite o celular do cliente")]
        [Phone(ErrorMessage = "O celular informado não é válido!")]
        public string Celular { get; set; }
        [Required(ErrorMessage = "Digite o CPF ou CPNJ do cliente")]
        public string CPF_CNPJ { get; set; }
        public int EnderecoId { get; set; }        
        [Required(ErrorMessage = "Digite o endereço do cliente")]
        public EnderecoModel Endereco { get; set; }
    }
}
