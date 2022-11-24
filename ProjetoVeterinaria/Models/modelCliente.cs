using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoVeterinaria.Models
{
    public class modelCliente
    {
        [Required(ErrorMessage = "Informe o campo")]
        [DisplayName("Nome do Cliente")]
        public string nomeCliente { get; set; }

        [Required(ErrorMessage = "Informe o campo")]
        [DisplayName("Telefone do Cliente")]
        public string telefoneCliente { get; set; }

        [Required(ErrorMessage = "Informe o campo")]
        [DisplayName("Endereço Cliente")]
        public string enderecoCliente { get; set; }

        [Required(ErrorMessage = "Informe o campo")]
        [DisplayName("CEP Cliente")]
        [RegularExpression(@"^\d{8}$|^\d{5}-\d{3}$", ErrorMessage = "O código postal deve ser no formato 00000000 ou 00000-000")]
        public string cepCliente { get; set; }
        [Required(ErrorMessage = "Informe o campo")]
        public string codCliente { get; set; }
        [Required(ErrorMessage = "Informe o campo")]
        [DisplayName("Usuario")]
        public string usuario { get; set; }
        [Required(ErrorMessage = "Informe a senha")]
        [DisplayName("Senha")]
        public string senha { get; set; }
        [Required(ErrorMessage = "Informe o tipo de usuario")]
        public int TipoUsuario = 0;
        [Required(ErrorMessage = "Informe o usuario padrão")]
        public int usuariopadrao = 2;
        // lembrando que no banco de dados 1 é ADM e 2 é usuario normal, então tem que usar os condicionais para
        // ver o que cada usuario vai ver
    }
}