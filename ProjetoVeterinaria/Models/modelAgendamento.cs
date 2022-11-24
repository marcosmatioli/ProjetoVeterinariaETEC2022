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
        [Required(ErrorMessage = "Informe o codigo do agendamento")]
        [DisplayName("Codigo do Agendamento")]
        public string codAgendamento { get; set; }

        [Required(ErrorMessage = "Informe o codigo do animal")]
        [DisplayName("Codigo do Animal")]
        public string codAnimal { get; set; }

        [Required(ErrorMessage = "Informe o codigo do veterinario")]
        [DisplayName("Codigo do Veterinario")]
        public string codVet { get; set; }

        [Required(ErrorMessage = "Informe a reclamação")]
        [DisplayName("Reclamação")]
        public string Reclamacao { get; set; }

        [Required(ErrorMessage = "Informe a hora do agendamento")]
        [DisplayName("Hora do agendamento")]
        public string horaAgendamento { get; set; }

        [DisplayName("Data do Agendamento")]
        [Required(ErrorMessage = "Informe a data do agendamento")]
        public string dataAgendamento { get; set; }

        [Required(ErrorMessage = "Informe o campo")]
        public bool TemAgendamento { get; set; }

        [Required(ErrorMessage = "Informe o nome do veterinario")]
        public string nomeVet { get; set; }

        [Required(ErrorMessage = "Informe do animal")]
        public string nomeAnimal { get; set; }

        [Required(ErrorMessage = "Informe o codigo do cliente")]
        public string codCliente { get; set; }

        [Required(ErrorMessage = "Informe o nome do cliente")]
        public string nomeCliente { get; set; }
    }
}