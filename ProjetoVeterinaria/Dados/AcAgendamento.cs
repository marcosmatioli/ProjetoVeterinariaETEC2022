using MySql.Data.MySqlClient;
using ProjetoVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ProjetoVeterinaria.Dados
{
    public class AcAgendamento
    {
        AcConexao con = new AcConexao();
        public void TemAgendamento(modelAgendamento mod)
        {
            mod.TemAgendamento = false;
            MySqlCommand cmd = new MySqlCommand
             ("select * from tbAgendamento where dataAgendamento =" +
             " @dataAgendamento and horaAgendamento = @horaAgendamento", con.MyConectarBD());
            cmd.Parameters.Add("@horaAgendamento", MySqlDbType.VarChar).Value = mod.horaAgendamento;
            cmd.Parameters.Add("@dataAgendamento", MySqlDbType.VarChar).Value = mod.dataAgendamento;

            MySqlDataReader leitor;
            leitor = cmd.ExecuteReader();
            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    mod.TemAgendamento = false;
                    // se ele encontrar um igual ele coloca false então não é para agendar
                }
            }
            else
            {
                mod.TemAgendamento = true;
                //se for verdade é pq ele não encontrou nenhum caso no banco então pode agendar
            }
            con.MyDesConectarBD();
        }
        public void cadAgendamento(modelAgendamento mod)
        {

            MySqlCommand cmd =
            new MySqlCommand("insert into tbAgendamento " +
            "(codAgendamento, dataAgendamento, horaAgendamento, codAnimal, codVet, Reclamacao,codCliente)" +
                " values (@codAgendamento, @dataAgendamento, @horaAgendamento, @codAnimal, @codVet, @Reclamacao,@codCliente)", con.MyConectarBD()); // @: PARAMETRO
            
            cmd.Parameters.Add("@codAgendamento", MySqlDbType.VarChar).Value = mod.codAgendamento;
            cmd.Parameters.Add("@dataAgendamento", MySqlDbType.VarChar).Value = mod.dataAgendamento;
            cmd.Parameters.Add("@horaAgendamento", MySqlDbType.VarChar).Value = mod.horaAgendamento;
            cmd.Parameters.Add("@codAnimal", MySqlDbType.VarChar).Value = mod.codAnimal;
            cmd.Parameters.Add("@codVet", MySqlDbType.VarChar).Value = mod.codVet;
            cmd.Parameters.Add("@Reclamacao", MySqlDbType.VarChar).Value = mod.Reclamacao;
            cmd.Parameters.Add("@codCliente", MySqlDbType.VarChar).Value = mod.codCliente;

            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }
        public List<modelAgendamento> GetAgendamento(modelAgendamento cm)
        {
            //pegar tudo na tabela animal
            List<modelAgendamento> AgendamentoList = new List<modelAgendamento>();
            MySqlCommand cmd = new MySqlCommand("select tbAgendamento.codAgendamento,tbAgendamento.dataAgendamento,tbAgendamento.horaAgendamento,tbAgendamento.Reclamacao,tbVeterinario.nomeVet,tbAgendamento.codVet,tbAnimal.nomeAnimal,tbCliente.nomeCliente from tbAgendamento\r\ninner join tbAnimal on tbAnimal.codAnimal = tbAgendamento.codAnimal inner join tbVeterinario on tbAgendamento.codVet = tbVeterinario.codVet inner join tbCliente on tbCliente.codCliente = tbAgendamento.codCliente where tbAgendamento.codCliente = @codCliente;", con.MyConectarBD());

            cmd.Parameters.Add("@codCliente", MySqlDbType.VarChar).Value = cm.codCliente;

            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                AgendamentoList.Add(
                       new modelAgendamento
                       {
                           codAgendamento = Convert.ToString(dr["codAgendamento"]),
                           dataAgendamento = Convert.ToString(dr["dataAgendamento"]),
                           horaAgendamento = Convert.ToString(dr["horaAgendamento"]),
                           Reclamacao = Convert.ToString(dr["Reclamacao"]),
                           nomeAnimal = Convert.ToString(dr["nomeAnimal"]),
                           nomeVet = Convert.ToString(dr["nomeVet"]),
                           codVet = Convert.ToString(dr["codVet"]),
                           nomeCliente = Convert.ToString(dr["nomeCliente"])
                       });
            }
            return AgendamentoList;
        }
        public List<modelAgendamento> GetAgendamentoADM(modelAgendamento cm)
        {
            //pegar tudo na tabela animal
            List<modelAgendamento> AgendamentoList = new List<modelAgendamento>();
            MySqlCommand cmd = new MySqlCommand("select tbAgendamento.codAgendamento,tbAgendamento.dataAgendamento,tbAgendamento.horaAgendamento,tbAgendamento.Reclamacao,tbVeterinario.nomeVet,tbAgendamento.codVet,tbAnimal.nomeAnimal,tbCliente.nomeCliente from tbAgendamento  \r\ninner join tbAnimal on tbAnimal.codAnimal = tbAgendamento.codAnimal inner join tbVeterinario on tbAgendamento.codVet = tbVeterinario.codVet inner join tbCliente on tbAgendamento.codCliente = tbCliente.codCliente;", con.MyConectarBD());

            cmd.Parameters.Add("@codAgendamento", MySqlDbType.VarChar).Value = cm.codAgendamento;
            cmd.Parameters.Add("@codCliente", MySqlDbType.VarChar).Value = cm.codCliente;
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                AgendamentoList.Add(
                       new modelAgendamento
                       {
                           codAgendamento = Convert.ToString(dr["codAgendamento"]),
                           dataAgendamento = Convert.ToString(dr["dataAgendamento"]),
                           horaAgendamento = Convert.ToString(dr["horaAgendamento"]),
                           Reclamacao = Convert.ToString(dr["Reclamacao"]),
                           nomeVet = Convert.ToString(dr["nomeVet"]),
                           codVet = Convert.ToString(dr["codVet"]),
                           nomeAnimal = Convert.ToString(dr["nomeAnimal"]),
                           nomeCliente = Convert.ToString(dr["nomeCliente"])
                       });
            }
            return AgendamentoList;
        }
        public bool deleteAnimal(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbAnimal where codAnimal=@id", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@id", id);


            int i = cmd.ExecuteNonQuery();
            con.MyDesConectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool atualizarAnimal(modelAnimal cm)
        {
            MySqlCommand cmd = new MySqlCommand("update tbAnimal set nomeAnimal=@nomeAnimal,fotoAnimal=@fotoAnimal,codTipoAnimal=@codTipoAnimal,codCliente=@codCliente where codAnimal=@codAnimal", con.MyConectarBD());

            cmd.Parameters.Add("@nomeAnimal", MySqlDbType.VarChar).Value = cm.nomeAnimal;
            cmd.Parameters.Add("@fotoAnimal", MySqlDbType.VarChar).Value = cm.fotoAnimal;
            cmd.Parameters.Add("@codTipoAnimal", MySqlDbType.VarChar).Value = cm.codTipoAnimal;
            cmd.Parameters.Add("@codCliente", MySqlDbType.VarChar).Value = cm.codCliente;
            cmd.Parameters.Add("@codAnimal", MySqlDbType.VarChar).Value = cm.codAnimal;
            int i = cmd.ExecuteNonQuery();

            con.MyDesConectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}