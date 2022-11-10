using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ProjetoVeterinaria.Models
{
    public class modelAgendamento
    {
        [DisplayName("Codigo do Agendamento")]
        [Required]
        public string codAgendamento { get; set; }
        [DisplayName("Codigo do Animal")]
        [Required]
        public string codAnimal { get; set; }
        [DisplayName("Codigo do Veterinario")]
        [Required]
        public string codVet { get; set; }
        [DisplayName("Problema que apararenta o animal")]
        [Required]
        public string Reclamacao { get; set; }
        [DisplayName("Hora do Atendimento")]
        [Required]
        public string horaAgendamento { get; set; }
        [DisplayName("Data do Agendamento")]
        [Required]
        public string dataAgendamento { get; set; }
        public bool TemAgendamento { get; set; }
        public string nomeVet { get; set; }
        public string nomeAnimal { get; set; }
        public string codCliente { get; set; }
        public string nomeCliente { get; set; }
    }
}