using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ProjetoVeterinaria.Models
{
    public class modelAnimal
    {
        [DisplayName("Codigo do Animal")]
        public string codAnimal { get; set; }
        [DisplayName("Nome do Animal")]
        public string nomeAnimal { get; set; }
        [DisplayName("Foto do Animal")]
        public string fotoAnimal { get; set; }
        [DisplayName("Codigo do Tipo de Animal")]
        public string codTipoAnimal { get; set; }
        [DisplayName("Codigo do Cliente")]
        public string codCliente { get; set; }
        [DisplayName("Nome do Cliente")]
        public string nomeCliente { get; set; }
    }
}