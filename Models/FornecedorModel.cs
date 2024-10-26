using System.ComponentModel.DataAnnotations;

namespace FazendaUrbana.Models
{
    public class FornecedorModel
    {        
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome do fornecedor")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite o e-mail do fornecedor")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite o celular do fornecedor")]
        [Phone(ErrorMessage = "O celular informado não é válido!")]
        public string Celular { get; set; }
        [Required(ErrorMessage = "Digite o CPF ou CPNJ do fornecedor")]
        public string CNPJ { get; set; }
        public int EnderecoId { get; set; }
        [Required(ErrorMessage = "Digite o endereço do fornecedor")]
        public EnderecoModel Endereco { get; set; }
    }
}
