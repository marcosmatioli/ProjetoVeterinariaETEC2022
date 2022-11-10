using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ProjetoVeterinaria.Models
{
    public class modelTipoAnimal
    {
        [DisplayName("Codigo Tipo Animal")]
        public string codTipoAnimal { get; set; }
        [DisplayName("Nome do tipo do Animal")]
        public string tipoAnimal { get; set; }

    }
}