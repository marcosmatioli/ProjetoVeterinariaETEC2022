using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoVeterinaria.Models
{
    public class modelAnimal
    {
        [Required(ErrorMessage = "Informe o codigo do animal")]
        [DisplayName("Codigo do Animal")]
        public string codAnimal { get; set; }
        [Required(ErrorMessage = "Informe o nome do animal")]
        [DisplayName("Nome do Animal")]
        public string nomeAnimal { get; set; }
        [DisplayName("Foto do Animal")]
        public string fotoAnimal { get; set; }
        [Required(ErrorMessage = "Informe o codigo do tipo do animal")]
        [DisplayName("Codigo do Tipo de Animal")]
        public string codTipoAnimal { get; set; }
        [Required(ErrorMessage = "Informe o codigo do cliente")]
        [DisplayName("Codigo do Cliente")]
        public string codCliente { get; set; }
        [Required(ErrorMessage = "Informe o nome do cliente")]
        [DisplayName("Nome do Cliente")]
        public string nomeCliente { get; set; }
    }
}