using MySql.Data.MySqlClient;
using ProjetoClinicaNoite2508.Dados;
using ProjetoClinicaNoite2508.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace ProjetoClinicaNoite2508.Controllers
{
    
    public class ClinicaController : Controller
    {
        
        // GET: Clinica
        AcPaciente acPaciente = new AcPaciente();
        AcMedico acMedico = new AcMedico();
        AcEspecialidade acEspecialidade = new AcEspecialidade();    
        AcAtendimento AcAtendimento = new AcAtendimento();
        AcConsultar AcConsultar = new AcConsultar();
        AcAgendamento AcAgendamento = new AcAgendamento();

        public void CarregaMedico()
        {
            List<SelectListItem> medico = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bdClinica; User=root;pwd=rootroot1995.M"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbMedico", con);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    medico.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }
            ViewBag.Medico = new SelectList(medico, "Value", "Text");
        }
        public void CarregaPaciente()
        {
            List<SelectListItem> paciente = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bdClinica; User=root;pwd=rootroot1995.M"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbPaciente", con);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    
                    paciente.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                    
                }
                con.Close();
                con.Open();
            }
            ViewBag.paciente = new SelectList(paciente, "Value", "Text");
        }
        public void CarregaEspecialidade()
        {
            List<SelectListItem> especialidade = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bdClinica; User=root;pwd=rootroot1995.M"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbEspecialidade", con);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    especialidade.Add(new SelectListItem{
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }
            ViewBag.Especialidade = new SelectList(especialidade, "Value", "Text");
        }

        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult CadPaciente()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CadPaciente(ModelPaciente mod)
        {
            acPaciente.inserirPaciente(mod);
            ViewBag.msg = "Cadastro do paciente efetuado com sucesso";
            return View();
        }

        public ActionResult CadMedico()
        {
            CarregaEspecialidade();
            return View();
        }
        [HttpPost]
        public ActionResult CadMedico(ModelMedico mod)
        {
            CarregaEspecialidade();
            mod.codEspecialidade = Request["Especialidade"]; //pega do droplist da cadMedico
            acMedico.inserirMedico(mod);
            ViewBag.msg = "Cadastrado do Medico feito com sucesso";
           // ViewBag.msg = mod.codEspecialidade; //teste que fiz para saber se estava vindo o valor correto
            return View();
        }

        public ActionResult CadEspecialidade()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CadEspecialidade(ModelEspecialidade mod)
        {
            acEspecialidade.inserirEspecialidade(mod);
            ViewBag.msg = "Cadastro da especialidade feita com sucessa";
            return View();
        }
        public ActionResult CadAtendimento()
        {
            CarregaMedico();
            CarregaPaciente();
            return View();
        }
        [HttpPost]
        public ActionResult CadAtendimento(ModelAtendimento mod)
        {
            CarregaMedico();
            CarregaPaciente();

            mod.codMedico = Request["Medico"];
            mod.codPaciente = Request["Paciente"];
            AcAtendimento.inserirAtendimento(mod);
            ViewBag.msg = "Atendimento Médico cadastrado";


            //PARTE DO CODIGO PARA COMPARAR SE AS DATAS SÃO IGUAIS
            //AcAtendimento.TemConsulta(mod);
            //if (mod.TemConsulta)
            //{
            //    AcAtendimento.inserirAtendimento(mod);
            //    ViewBag.msg = "Horario Agendado com sucesso";
            //}
            //else
            //{
            //    ViewBag.msg = "Horario indisponivel";
            //}
            return View();
        }
        public ActionResult ConsultarTudo()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ConsultarTudo(ModelConsultar mod)
        {

            GridView dgv = new GridView(); // Instância para a tabela 
            dgv.DataSource = AcConsultar.ConsultaTable(mod); //Atribuir ao grid o resultado da consulta 
            dgv.DataBind(); //Confirmação do Grid 
            StringWriter sw = new StringWriter(); //Comando para construção do Grid na tela 
            HtmlTextWriter htw = new HtmlTextWriter(sw); //Comando para construção do Grid na tela 
            dgv.RenderControl(htw); //Comando para construção do Grid na tela 
            ViewBag.GridViewString = sw.ToString(); //Comando para construção do Grid na tela
            //ViewBag.msg = "Consulta realizada com sucesso";
            return View();
        }
    
        public ActionResult CadAgendamento()
        {
            CarregaMedico();
            CarregaPaciente();
            return View();
        }
        [HttpPost]
        public ActionResult CadAgendamento(ModelAgendamento mod)
        {
            CarregaMedico();
            CarregaPaciente();

            mod.codMedico = Request["Medico"];
            mod.codPaciente = Request["Paciente"];

            AcAgendamento.ConsultarAgendamento(mod);

            if (mod.ConfigAgendamento)
            {
                AcAgendamento.inserirAgendamento(mod);
                ViewBag.msg = "Agendamento realizado com sucesso";
            }
            else
            {
                ViewBag.msg = "Horario escolhido indisponivel, por favor escolha outro";
            }
            return View();
        }
    }
    
}