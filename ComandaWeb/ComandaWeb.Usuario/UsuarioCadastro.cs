using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComandaWeb.DAL.Usuario
{
    public class UsuarioCadastro
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmação de senha")]
        [Compare("ConfirmarSenha", ErrorMessage = "Senha e confirmação são diferentes.")]
        public string ConfirmarSenha { get; set; }
    }
}
