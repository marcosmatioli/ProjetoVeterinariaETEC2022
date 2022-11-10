using MySql.Data.MySqlClient;
using ProjetoVeterinaria.Dados;
using ProjetoVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.UI;
using static System.Net.WebRequestMethods;
using K4os.Compression.LZ4.Streams;
using MySqlX.XDevAPI;

namespace ProjetoVeterinaria.Controllers
{
    public class HomeController : Controller
    {
        public string codClienteControle = "";
        AcAnimal acAnimal = new AcAnimal();
        AcCliente acCliente = new AcCliente();
        acTipoAnimal acTipoAnimal = new acTipoAnimal();
        acVeterinario acVeterinario = new acVeterinario();
        AcConsultar AcConsultar = new AcConsultar();
        AcAtendimento AcAtendimento = new AcAtendimento();
        AcAgendamento acAgendamento = new AcAgendamento();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(modelCliente mod)
        {
            acCliente.TestarUsuario(mod);
            if (mod.usuario != null && mod.senha != null && mod.TipoUsuario != 0)
            {
                Session["usuarioLogado"] = mod.usuario.ToString();
                Session["senhaLogado"] = mod.senha.ToString();
                Session["tipoLogado"] = mod.TipoUsuario.ToString();
                Session["codClienteLogado"] = acCliente.GetcodCliente(mod);

                if (Session["tipoLogado"].ToString() == "1")
                {// basicamente esse if analise se o usuario é adm ou não e ja redireciona para a pagina dele
                    return RedirectToAction("About", "Home");
                }
                else
                {
                    return RedirectToAction("Contact", "Home");
                }
            }
            else
            {// vai cair nesse caso se o usuario não for encontrado
                ViewBag.Message = "Usuario ou senha incorretos - Verifique e efetue o login novamente";
                return View();
            }
        }

        public ActionResult About()
        {   // usuario do tipo 1 é ADM, conforme no banco de dados
            //para colocar as coisas aqui para o usuario ADM
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null || Session["tipoLogado"] == null)
            {
                //se estiver null ele está tentando acessar a pagina sem logar, mesmo que seja uma pagina de usuario
                //sem logar ele não pode entrar
                return RedirectToAction("Index", "Home");
            }
            else
            {// se os valores não estiverem nulos eñtao tem algo , ai ele olha se o valor é de 
                if (Session["tipoLogado"].ToString() != "1")
                {
                    return RedirectToAction("semAcesso", "Home");
                }
                else
                {
                    ViewBag.Message = "Bem vindo Administrador";
                    return View();
                }
            }

        }
        public ActionResult Contact()
        {   // usuario do tipo 2 é um usuario normal
            //colocar aqui coisas para o usuario normal
            //como fazer agendamento, consultar os agendamentos dela e ver os animais dela
            if (Session["usuarioLogado"] == null || Session["senhaLogado"] == null || Session["tipoLogado"] == null)
            {
                // se os valores da session for null, então ele redireciona para o index para fazer login denovo
                ViewBag.Message = "Efetue o login para acessar os menus.";
                return RedirectToAction("Index", "Home");
            }
            else
            {   // se não ele entra na pagina com sucesso
                ViewBag.Message = "Bem vindo, usuario !!";
                return View();
            }
        }
        public ActionResult Logout()
        {
            Session["usuarioLogado"] = null;
            Session["senhaLogado"] = null;
            Session["tipoLogado"] = null;
            Session["codClienteLogado"] = null;
            ViewBag.Message = "Deslogou com sucesso";
            return RedirectToAction("Index", "Home");
        }
        public ActionResult semACesso()
        {
            ViewBag.Message = "Você não tem acesso a essa página";
            return RedirectToAction("Index", "Home");
        }
        public void CarregarVeterinario()
        {
            List<SelectListItem> Vet = new List<SelectListItem>();
            using (MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bdClinicaVeterinaria; User=root;pwd=12345678"))
          //  using (MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bdClinicaVeterinaria; User=root;pwd=rootroot1995.M"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbVeterinario", con);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Vet.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }
            ViewBag.Vet = new SelectList(Vet, "Value", "Text");
        }
        public void CarregarAnimal() // vai ter que criar um carregarAnimal para o ADM 
        {
            codClienteControle = Convert.ToString(Session["codClienteLogado"]); 
            List<SelectListItem> Animal = new List<SelectListItem>();
            using (MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bdClinicaVeterinaria; User=root;pwd=12345678"))
           // using (MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bdClinicaVeterinaria; User=root;pwd=rootroot1995.M"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbAnimal where codCliente = @codCliente", con);
                cmd.Parameters.Add("@codCliente", MySqlDbType.VarChar).Value = codClienteControle;
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Animal.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }
            ViewBag.Animal = new SelectList(Animal, "Value", "Text");
        }
        public void CarregarAnimalADM() // vai ter que criar um carregarAnimal para o ADM 
        {
            List<SelectListItem> Animal = new List<SelectListItem>();
            using (MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bdClinicaVeterinaria; User=root;pwd=12345678"))
            // using (MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bdClinicaVeterinaria; User=root;pwd=rootroot1995.M"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbAnimal", con);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Animal.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }
            ViewBag.Animal = new SelectList(Animal, "Value", "Text");
        }
        public void CarregarCliente()
        {
            List<SelectListItem> cliente = new List<SelectListItem>();
            using (MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bdClinicaVeterinaria; User=root;pwd=12345678"))
           // using (MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bdClinicaVeterinaria; User=root;pwd=rootroot1995.M"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbCliente", con);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    cliente.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }
            ViewBag.Cliente = new SelectList(cliente, "Value", "Text");
        }
        public void CarregarTipoAnimal()
        {
            List<SelectListItem> TipoAnimal = new List<SelectListItem>();
            
            using (MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bdClinicaVeterinaria; User=root;pwd=12345678"))
           // using (MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bdClinicaVeterinaria; User=root;pwd=rootroot1995.M"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tbTipoAnimal", con);
                MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    TipoAnimal.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }
            ViewBag.TipoAnimal = new SelectList(TipoAnimal, "Value", "Text");
        }
        public ActionResult cadClienteADM()
        {
            return View();
        }
        [HttpPost]
        public ActionResult cadClienteADM(modelCliente mod)
        {
            mod.TipoUsuario = Convert.ToInt16(Request["TipoUsuario"]);
            acCliente.cadastrarClienteADM(mod);
            ViewBag.msg = "Cadastro do cliente realizado com sucesso!";
            return View();
        }
        public ActionResult cadCliente()
        {
            return View();
        }
        [HttpPost]
        public ActionResult cadCliente(modelCliente mod)
        {
            acCliente.cadastrarCliente(mod);
            ViewBag.msg = "Cadastro do cliente realizado com sucesso!";
            return View();
        }
        public ActionResult cadAnimal()
        {
            CarregarCliente();
            CarregarTipoAnimal();
            return View();
        }
        [HttpPost]
        public ActionResult cadAnimal(modelAnimal mod, HttpPostedFileBase file)
        {
            CarregarCliente();
            CarregarTipoAnimal();
            mod.codCliente = Convert.ToString(Session["codClienteLogado"]);
            //mod.codCliente = Request["Cliente"];
            mod.codTipoAnimal = Request["TipoAnimal"];
            //parte que pega e cria o caminho do arquivo
            // quando cadastra tem que fazer um if aqui para ver se a pessoa escolheu a foto,
            // se não escolheu foto tem que por uma padrão
            // se o file que no caso é o arquivo da pessoa for diferente de null então ele vai guardar
            //FUNCIONOU, testado e aprovado por mim MARCOS MATIOLI
            if (file != null)
            {
                string arquivo = Path.GetFileName(file.FileName);
                string file2 = "/Imagens/" + Path.GetFileName(file.FileName);
                string _path = Path.Combine(Server.MapPath("~/Imagens"), arquivo);
                file.SaveAs(_path);
                mod.fotoAnimal = file2;
            }
            else // se for null então ele vai colocar a imagem padrão
            {
                //string arquivo = Path.GetFileName(file.FileName);
                string file2 = "/Imagens/image-not-found.jpg";
                //string _path = Path.Combine(Server.MapPath("~/Imagens"), arquivo);
                //file.SaveAs(_path);
                mod.fotoAnimal = file2;
            }

            acAnimal.cadastrarAnimal(mod);
            ViewBag.msg = "Cadastro do animal realizado com sucesso!";
            return View();
        }
        public ActionResult cadTipoAnimal()
        {
            return View();
        }
        [HttpPost]
        public ActionResult cadTipoAnimal(modelTipoAnimal mod)
        {
            acTipoAnimal.cadastrarTipoAnimal(mod);
            ViewBag.msg = "Cadastro do tipo de animal realizado com sucesso!";
            return View();
        }
        public ActionResult cadVeterinario()
        {
            return View();
        }
        [HttpPost]
        public ActionResult cadVeterinario(modelVeterinario mod)
        {
            acVeterinario.cadastrarVeterinario(mod);
            ViewBag.msg = "Cadastro do Veterinario realizado com sucesso!";
            return View();
        }
        //public ActionResult cadListarPets(modelAnimal cm,modelCliente mod)
        //{
        //    CarregarCliente();
        //    return View(acAnimal.GetPet(cm, mod)) ;
        //}
        public ActionResult ListarTipoAnimal()
        {
            return View(acTipoAnimal.GetTipoAnimal());
        }
        public ActionResult excluirTipoAnimal(int id)
        {
            acTipoAnimal.DeleteTipoAnimal(id);
            return RedirectToAction("ListarTipoAnimal");
        }
        public ActionResult editarTipoAnimal(string id)
        {
            return View(acTipoAnimal.GetTipoAnimal().Find(model => model.codTipoAnimal == id));
        }
        [HttpPost]
        public ActionResult editarTipoAnimal(int id, modelTipoAnimal cm)
        {
            cm.codTipoAnimal = id.ToString();
            acTipoAnimal.atualizarTipoAnimal(cm);
            ViewBag.msg = "Edição feita com sucesso";
            return View();
        }
        public ActionResult ConsultarTabelasGrid()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ConsultarTabelasGrid(modelConsultar mod)
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
        public ActionResult cadAtendimento()
        {
            CarregarAnimal();
            CarregarVeterinario();
            return View();
        }
        [HttpPost]
        public ActionResult cadAtendimento(modelAtendimento mod)
        {
            CarregarVeterinario();
            CarregarAnimal();

            mod.codVet = Request["Vet"];
            mod.codAnimal = Request["Animal"];
            mod.codCliente = Convert.ToString(Session["codClienteLogado"]);
            //PARTE DO CODIGO PARA COMPARAR SE AS DATAS SÃO IGUAIS CTRL+K e dps CTRL+U / U= descomenta / C= Comenta 
            AcAtendimento.TemAtendimento(mod);
            if (mod.TemAtendimento)
            {
                AcAtendimento.cadAtendimento(mod);
                ViewBag.msg = "Horario do Atendimento Disponivel, cadastro realizado Sucesso!";
            }
            else
            {
                ViewBag.msg = "Horario do Atendimento Indisponivel, cadastro não realizado. Por favor escolha outra data/horario.";
            }
            return View();
        }
        public ActionResult cadAgendamento()
        {
            codClienteControle = Convert.ToString(Session["codClienteLogado"]);
            if (codClienteControle == "1")
            {
                CarregarAnimalADM();
            }
            else
            {
                CarregarAnimal();
            }
            CarregarVeterinario();
            return View();
        }
        [HttpPost]
        public ActionResult cadAgendamento(modelAgendamento mod)
        { 
            codClienteControle = Convert.ToString(Session["codClienteLogado"]); 
            if (codClienteControle == "1")
            {
                CarregarAnimalADM();
            }
            else
            {
                CarregarAnimal();
            }

            CarregarVeterinario();
            

            mod.codVet = Request["Vet"];
            mod.codAnimal = Request["Animal"];
            mod.codCliente = Convert.ToString(Session["codClienteLogado"]);
            //PARTE DO CODIGO PARA COMPARAR SE AS DATAS SÃO IGUAIS CTRL+K e dps CTRL+U / U= descomenta / C= Comenta 
            acAgendamento.TemAgendamento(mod);
            if (mod.TemAgendamento)
            {
                acAgendamento.cadAgendamento(mod);
                ViewBag.Message = "Horario de agendamento disponivel, agendamento com sucesso";
            }
            else
            {
                ViewBag.Message = "Horario do agendamento indisponivel, agendamento não realizado. Por favor escolha outra data/horario.";
            }
            return View();
        }
        public ActionResult ListarAnimalUsuario(modelCliente mod)
        {
            codClienteControle = Convert.ToString(Session["codClienteLogado"]);
            //codClienteControle = acCliente.GetcodCliente(mod);
            return View(acCliente.GetAnimalUsuario(mod,codClienteControle));
        }
        public ActionResult excluirAnimal(int id)
        {
            acAnimal.deleteAnimal(id);
            return RedirectToAction("ListarAnimalUsuario");
        }
        public ActionResult editarAnimal(string id,modelAnimal cm,modelCliente mod)
        {
            CarregarAnimal();
            CarregarCliente();
            CarregarTipoAnimal();
            return View(acAnimal.GetPet(cm,mod).Find(model => model.codAnimal == id));
        }
        [HttpPost]
        public ActionResult editarAnimal(string id, modelAnimal mod, HttpPostedFileBase file)
        {
            CarregarCliente();
            CarregarTipoAnimal();
            mod.codCliente = Convert.ToString(Session["codClienteLogado"]);
            mod.codTipoAnimal = Request["TipoAnimal"];
            if (file != null)
            {
                string arquivo = Path.GetFileName(file.FileName);
                string file2 = "/Imagens/" + Path.GetFileName(file.FileName);
                string _path = Path.Combine(Server.MapPath("~/Imagens"), arquivo);
                file.SaveAs(_path);
                mod.fotoAnimal = file2;
            }
            else 
            {
                string file2 = "/Imagens/image-not-found.jpg";
                mod.fotoAnimal = file2;
            }    
            mod.codAnimal = id.ToString();
            acAnimal.atualizarAnimal(mod);
            ViewBag.msg = "Edição feita com sucesso";
            return View();
        }
        public ActionResult editarCliente(string id, modelCliente mod)
        {
            mod.codCliente = Convert.ToString(Session["codClienteLogado"]);
            return View();
        }
        [HttpPost]
        public ActionResult editarCliente(modelCliente mod)
        {
            mod.codCliente = Convert.ToString(Session["codClienteLogado"]);
            acCliente.editarCliente(mod);
            ViewBag.msg = "Cadastro atualizado com sucesso!";
            return View();
        }
        public ActionResult listarAgendamento(modelAgendamento mod)
        {
            mod.codCliente = Convert.ToString(Session["codClienteLogado"]);
            return View(acAgendamento.GetAgendamento(mod));
        }
        public ActionResult listarAgendamentoADM(modelAgendamento mod)
        {
            mod.codCliente = Convert.ToString(Session["codClienteLogado"]);
            return View(acAgendamento.GetAgendamentoADM(mod));
        }
        public ActionResult listarAtendimento(modelAtendimento mod)
        {
            mod.codCliente = Convert.ToString(Session["codClienteLogado"]);
            return View(AcAtendimento.GetAtendimento(mod));
        }
        public ActionResult listarAtendimentoADM(modelAtendimento mod)
        {
            mod.codCliente = Convert.ToString(Session["codClienteLogado"]);
            return View(AcAtendimento.GetAtendimentoADM(mod));
        }
    }
}