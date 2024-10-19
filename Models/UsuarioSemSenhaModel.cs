using FazendaUrbana.Enums;
using System.ComponentModel.DataAnnotations;

namespace FazendaUrbana.Models
{
    public class UsuarioSemSenhaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome do usuário")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Digite o CPF do usuário")]
        public string CPF { get; set; }
        [Required(ErrorMessage = "Digite a data de nascimento do usuário")]
        public string DataNascimento { get; set; }
        [Required(ErrorMessage = "Digite o login do usuário")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Digite o email do usuário")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Selecione o perfil do usuário")]
        public PerfilEnum? Perfil { get; set; }
        
    }
}
