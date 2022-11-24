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
        [Required(ErrorMessage = "Informe o codigo do atendimento")]
        public string codAtendimento { get; set; }
        [DisplayName("Codigo do Animal")]
        [Required(ErrorMessage = "Informe o codigo do animal")]
        public string codAnimal { get; set; }
        [DisplayName("Codigo do Veterinario")]
        [Required(ErrorMessage = "Informe o codigo do veterinario")]
        public string codVet { get; set; }
        [DisplayName("Diagnostico do Animal")]
        [Required(ErrorMessage = "Informe o diagnostico")]
        public string Diagnostico { get; set; }
        [DisplayName("Hora do Atendimento")]
        [Required(ErrorMessage = "Informe o hora do atendimento")]
        public string horaAtendimento { get; set; }
        [Required(ErrorMessage = "Informe o data do atendimento")]
        public string dataAtendimento { get; set; }
        
        public bool TemAtendimento { get; set; }
        [Required(ErrorMessage = "Informe o nome do veterinario")]
        public string nomeVet { get; set; }
        [Required(ErrorMessage = "Informe o nome do animal")]
        public string nomeAnimal { get; set; }
        [Required(ErrorMessage = "Informe o codigo do cliente")]
        public string codCliente { get; set; }
        [Required(ErrorMessage = "Informe o nome do cliente")]
        public string nomeCliente { get; set; }
    }
}