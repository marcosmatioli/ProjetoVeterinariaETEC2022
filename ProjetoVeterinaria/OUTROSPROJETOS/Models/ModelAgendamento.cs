using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ProjetoClinicaNoite2508.Models
{
    public class ModelAgendamento
    {
        public string codAgendamento { get; set; }
        public string codPaciente { get; set; }
        public string codMedico { get; set; }
        public string horaAgendamento   { get; set; }
        public string dataAgendamento { get; set; }
        public string reclamacaoPaciente { get; set; }
        public bool ConfigAgendamento { get; set; }


    }
}