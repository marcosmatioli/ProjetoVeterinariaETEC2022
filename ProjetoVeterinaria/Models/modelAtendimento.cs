using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.EnterpriseServices;
using System.Linq;
using System.Web;

namespace ProjetoVeterinaria.Models
{
    public class modelAtendimento
    {
        
        [DisplayName("Codigo do Atendimento")]
        [Required]
        public string codAtendimento { get; set; }
        [DisplayName("Codigo do Animal")]
        [Required]
        public string codAnimal { get; set; }
        [DisplayName("Codigo do Veterinario")]
        [Required]
        public string codVet { get; set; }
        [DisplayName("Diagnostico do Animal")]
        [Required]
        public string Diagnostico { get; set; }
        [DisplayName("Hora do Atendimento")]
        [Required]
        public string horaAtendimento { get; set; }
        [DisplayName("Data do Atendimento")]
        [Required]
        public string dataAtendimento { get; set; }
        public bool TemAtendimento { get; set; }
        public string nomeVet { get; set; }
        public string nomeAnimal { get; set; }
        public string codCliente { get; set; }
        public string nomeCliente { get; set; }
    }
}