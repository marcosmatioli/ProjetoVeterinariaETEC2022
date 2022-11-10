using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ProjetoVeterinaria.Models
{
    public class modelVeterinario
    {
        [DisplayName("Codigo do Veterinario")]
        public int codVet { get; set; }
        [DisplayName("Nome do Veterinario")]
        public string nomeVet { get; set; }
    }
}