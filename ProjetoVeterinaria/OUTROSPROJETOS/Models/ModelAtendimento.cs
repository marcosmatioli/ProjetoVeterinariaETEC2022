using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ProjetoClinicaNoite2508.Models
{
    public class ModelAtendimento
    {
        [DisplayName("Codigo Atendimento")]
        public string codAtendimento { get; set; }
        [DisplayName("Data Atendimento")]
        public string dataAtendimento { get; set; }

        [DisplayName("Codigo Paciente")]
        public string codPaciente { get; set; }

        [DisplayName("Codigo Medico")]
        public string codMedico { get; set; }

        [DisplayName("Diagnostico")]
        public string Diagnostico { get; set; }

        [DisplayName("Hora Atendimento")]
        public string horaAtendimento{ get; set; }

        public bool TemConsulta { get; set; }
    }
}