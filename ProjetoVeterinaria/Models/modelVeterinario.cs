using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoVeterinaria.Models
{
    public class modelVeterinario
    {
        [Required(ErrorMessage = "Informe o codigo do veterinario")]
        [DisplayName("Codigo do Veterinario")]
        public int codVet { get; set; }
        [Required(ErrorMessage = "Informe o nome do veterinario")]
        [DisplayName("Nome do Veterinario")]
        public string nomeVet { get; set; }
    }
}