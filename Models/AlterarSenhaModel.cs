﻿using System.ComponentModel.DataAnnotations;

namespace FazendaUrbana.Models
{
    public class AlterarSenhaModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite a nova senha do usuário")]
        public string NovaSenha { get; set; }
        [Required(ErrorMessage = "Confirme a nova senha do usuário")]
        [Compare("NovaSenha", ErrorMessage = "Senha não confere com a nova senha")]
        public string ConfirmarNovaSenha { get; set; }
    }
}
