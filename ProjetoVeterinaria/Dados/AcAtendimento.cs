using MySql.Data.MySqlClient;
using ProjetoVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;

namespace ProjetoVeterinaria.Dados
{
    public class AcAtendimento
    {
        AcConexao con = new AcConexao();
        public void TemAtendimento(modelAtendimento mod)
        {
            mod.TemAtendimento = false;
            MySqlCommand cmd = new MySqlCommand
             ("select * from tbAtendimento where dataAtendimento =" +
             " @dataAtendimento and horaAtendimento = @horaAtendimento", con.MyConectarBD());
            cmd.Parameters.Add("@horaAtendimento", MySqlDbType.VarChar).Value = mod.horaAtendimento;
            cmd.Parameters.Add("@dataAtendimento", MySqlDbType.VarChar).Value = mod.dataAtendimento;

            MySqlDataReader leitor;
            leitor = cmd.ExecuteReader();
            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    mod.TemAtendimento = false;
                    // se ele encontrar um igual ele coloca false então não é para agendar
                }
            }
            else
            {
                mod.TemAtendimento = true;
                //se for verdade é pq ele não encontrou nenhum caso no banco então pode agendar
            }
            con.MyDesConectarBD();
        }
        public void cadAtendimento(modelAtendimento mod)
        {

            MySqlCommand cmd =
            new MySqlCommand("insert into tbAtendimento " +
            "(codAtendimento, dataAtendimento, horaAtendimento, codAnimal, codVet, Diagnostico,codCliente)" +
                " values (@codAtendimento, @dataAtendimento, @horaAtendimento, @codAnimal, @codVet, @Diagnostico, @codCliente)", con.MyConectarBD()); // @: PARAMETRO

            cmd.Parameters.Add("@codAtendimento", MySqlDbType.VarChar).Value = mod.codAtendimento;
            cmd.Parameters.Add("@dataAtendimento", MySqlDbType.VarChar).Value = mod.dataAtendimento;
            cmd.Parameters.Add("@horaAtendimento", MySqlDbType.VarChar).Value = mod.horaAtendimento;
            cmd.Parameters.Add("@codAnimal", MySqlDbType.VarChar).Value = mod.codAnimal;
            cmd.Parameters.Add("@codVet", MySqlDbType.VarChar).Value = mod.codVet;
            cmd.Parameters.Add("@Diagnostico", MySqlDbType.VarChar).Value = mod.Diagnostico;
            cmd.Parameters.Add("@codCliente", MySqlDbType.VarChar).Value = mod.codCliente;

            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }
        public List<modelAtendimento> GetAtendimento(modelAtendimento cm)
        {
            
            List<modelAtendimento> AtendimentoList = new List<modelAtendimento>();
            MySqlCommand cmd = new MySqlCommand("select tbAtendimento.codAtendimento,tbAtendimento.dataAtendimento,tbAtendimento.horaAtendimento,tbAtendimento.Diagnostico,tbVeterinario.nomeVet,tbAtendimento.codVet,tbAnimal.nomeAnimal,tbCliente.nomeCliente from tbAtendimento inner join tbAnimal on tbAnimal.codAnimal = tbAtendimento.codAnimal inner join tbVeterinario on tbAtendimento.codVet = tbVeterinario.codVet inner join tbCliente on tbAtendimento.codCliente = tbCliente.codCliente where tbAtendimento.codCliente = @codCliente; ", con.MyConectarBD());

            cmd.Parameters.Add("@codCliente", MySqlDbType.VarChar).Value = cm.codCliente;

            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                AtendimentoList.Add(
                       new modelAtendimento
                       {
                           codAtendimento = Convert.ToString(dr["codAtendimento"]),
                           dataAtendimento = Convert.ToString(dr["dataAtendimento"]),
                           horaAtendimento = Convert.ToString(dr["horaAtendimento"]),
                           Diagnostico = Convert.ToString(dr["Diagnostico"]),
                           nomeAnimal = Convert.ToString(dr["nomeAnimal"]),
                           nomeVet = Convert.ToString(dr["nomeVet"]),
                           codVet = Convert.ToString(dr["codVet"]),
                           nomeCliente = Convert.ToString(dr["nomeCliente"])
                       });
            }
            return AtendimentoList;
        }
        public List<modelAtendimento> GetAtendimentoADM(modelAtendimento cm)
        {
            
            List<modelAtendimento> AtendimentoList = new List<modelAtendimento>();
            MySqlCommand cmd = new MySqlCommand("select tbAtendimento.codAtendimento,tbAtendimento.dataAtendimento,tbAtendimento.horaAtendimento,tbAtendimento.Diagnostico,tbVeterinario.nomeVet,tbAtendimento.codVet,tbAnimal.nomeAnimal,tbCliente.nomeCliente from tbAtendimento inner join tbAnimal on tbAnimal.codAnimal = tbAtendimento.codAnimal inner join tbVeterinario on tbAtendimento.codVet = tbVeterinario.codVet inner join tbCliente on tbAtendimento.codCliente = tbCliente.codCliente; ", con.MyConectarBD());

            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                AtendimentoList.Add(
                       new modelAtendimento
                       {
                           codAtendimento = Convert.ToString(dr["codAtendimento"]),
                           dataAtendimento = Convert.ToString(dr["dataAtendimento"]),
                           horaAtendimento = Convert.ToString(dr["horaAtendimento"]),
                           Diagnostico = Convert.ToString(dr["Diagnostico"]),
                           nomeAnimal = Convert.ToString(dr["nomeAnimal"]),
                           nomeVet = Convert.ToString(dr["nomeVet"]),
                           codVet = Convert.ToString(dr["codVet"]),
                           nomeCliente = Convert.ToString(dr["nomeCliente"])
                       });
            }
            return AtendimentoList;
        }
        public bool deleteAtendimento(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbAtendimento where codAtendimento=@id", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@id", id);


            int i = cmd.ExecuteNonQuery();
            con.MyDesConectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}