using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjetoVeterinaria.Models
{
    public class modelConsultar
    {
        [Required(ErrorMessage = "Informe o table consulta")]
        public string Tablenome { get; set; }
    }
}