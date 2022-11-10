using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ProjetoClinicaNoite2508.Models
{
    public class ModelPaciente
    {
     
        public string codPaciente { get; set; }

        [DisplayName("Nome do Paciente")] /*o texto que vai aparecer na view debaixo daqui, e ela está referenciada no cadPaciente*/
        public string nomePaciente { get; set; }

        [DisplayName("Endereço Paciente")]
        public string enderecoPaciente { get; set; }

        [DisplayName("Telefone Paciente")]
        public string telPaciente { get; set; }

        [DisplayName("Celular Paciente")]
        public string celPaciente { get; set; }
    }
}