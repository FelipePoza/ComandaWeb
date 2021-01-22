using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ComandaWeb.Seguranca
{
    public class LoginModel
    {
        [Required]
        [Display(Name = "Usuário")]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }
    }
}
