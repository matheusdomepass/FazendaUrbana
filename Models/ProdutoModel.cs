using System.ComponentModel.DataAnnotations;

namespace FazendaUrbana.Models
{
    public class ProdutoModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome do produto")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite a descrição do contato")]
        public string Descricao { get; set; }
        [Required(ErrorMessage = "Digite o valor do produto")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Valor { get; set; }
        [Required(ErrorMessage = "Digite a quantidade do produto")]
        public int Quantidade { get; set; }
        [Required(ErrorMessage = "Digite a categoria do produto")]
        public string Categoria { get; set; }
        public DateTime Add_Data { get; set; } = DateTime.Now;
        public string Add_Por { get; set; }
    }
}
