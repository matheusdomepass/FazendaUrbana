using System.ComponentModel.DataAnnotations;

namespace FazendaUrbana.Models
{
    public class EnderecoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome da rua do endereço")]
        public string Rua { get; set; }
        [Required(ErrorMessage = "Digite o numero do endereço")]
        public string Numero { get; set; }
        [Required(ErrorMessage = "Digite o complemento do endereço")]
        public string Complemento { get; set; }
        [Required(ErrorMessage = "Digite o bairro do endereço")]
        public string Bairro { get; set; }
        [Required(ErrorMessage = "Digite a cidade do endereço")]
        public string Cidade { get; set; }
        [Required(ErrorMessage = "Digite o estado do endereço")]
        public string Estado { get; set; }
        [Required(ErrorMessage = "Digite o CEP do endereço")]
        [RegularExpression(@"\d{5}-\d{3}", ErrorMessage = "O CEP deve estar no formato 00000-000.")]
        public string CEP { get; set; }
    }
}
