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
        [Required]        
        [DisplayName("Nome do Cliente")]
        public string nomeCliente { get; set; }
        [Required]
        [DisplayName("Telefone do Cliente")]
        public string telefoneCliente { get; set; }

        [Required]
        [DisplayName("Endereço Cliente")]
        public string enderecoCliente { get; set; }
        [Required]
        [DisplayName("CEP Cliente")]
        public string cepCliente { get; set; }
        public string codCliente { get; set; }
        [Required]
        [DisplayName("Usuario")]
        public string usuario { get; set; }
        [Required]
        [DisplayName("Senha")]
        public string senha { get; set; }
        [Required]
        public int TipoUsuario = 0;

        public int usuariopadrao = 2;
        // lembrando que no banco de dados 1 é ADM e 2 é usuario normal, então tem que usar os condicionais para
        // ver o que cada usuario vai ver
    }
}