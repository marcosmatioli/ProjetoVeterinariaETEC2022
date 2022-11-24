using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoVeterinaria.Models
{
    public class modelTipoAnimal
    {
        [Required(ErrorMessage = "Informe o codigo do animal")]
        [DisplayName("Codigo Tipo Animal")]
        public string codTipoAnimal { get; set; }
        [Required(ErrorMessage = "Informe o nome do tipo de animal")]
        [DisplayName("Nome do tipo do Animal")]
        public string tipoAnimal { get; set; }

    }
}