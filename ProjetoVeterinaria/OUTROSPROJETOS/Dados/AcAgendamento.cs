using MySql.Data.MySqlClient;
using ProjetoClinicaNoite2508.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoClinicaNoite2508.Dados
{
    public class AcAgendamento
    {
            Conexao con = new Conexao();

            public void inserirAgendamento(ModelAgendamento cm)
            {
                MySqlCommand cmd = 
                new MySqlCommand("insert into tbAgendamento " +
                "(codAgendamento, dataAgendamento, horaAgendamento, codPaciente, codMedico, reclamacaoPaciente)" +
                    " values (@codAgendamento, @dataAgendamento, @horaAgendamento, @codPaciente, @codMedico, @reclamacaoPaciente)", con.MyConectarBD()); // @: PARAMETRO

                cmd.Parameters.Add("@codAgendamento", MySqlDbType.VarChar).Value = cm.codAgendamento;
                cmd.Parameters.Add("@dataAgendamento", MySqlDbType.VarChar).Value = cm.dataAgendamento;
                cmd.Parameters.Add("@horaAgendamento", MySqlDbType.VarChar).Value = cm.horaAgendamento;
                cmd.Parameters.Add("@codPaciente", MySqlDbType.VarChar).Value = cm.codPaciente;
                cmd.Parameters.Add("@CodMedico", MySqlDbType.VarChar).Value = cm.codMedico;
                cmd.Parameters.Add("@reclamacaoPaciente", MySqlDbType.VarChar).Value = cm.reclamacaoPaciente;

                cmd.ExecuteNonQuery();
                con.MyDesConectarBD();
            }
        public void ConsultarAgendamento(ModelAgendamento mod)
        {
            MySqlCommand cmd = new MySqlCommand
                ("select * from tbAgendamento where dataAgendamento =" +
                " @dataAgendamento and horaAgendamento = @horaAgendamento", con.MyConectarBD());
            cmd.Parameters.Add("@horaAgendamento", MySqlDbType.VarChar).Value = mod.horaAgendamento;
            cmd.Parameters.Add("@dataAgendamento", MySqlDbType.VarChar).Value = mod.dataAgendamento;
            //cmd.Parameters.Add("@hora", MySqlDbType.VarChar).Value = agenda.horaAgen;
            MySqlDataReader leitor;
            leitor = cmd.ExecuteReader();
            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    mod.ConfigAgendamento = false;
                    // se ele encontrar um igual ele coloca false então não é para agendar
                }
            }
            else
            {
                mod.ConfigAgendamento = true;
                //se for verdade é pq ele não encontrou nenhum caso no banco então pode agendar
            }
            con.MyDesConectarBD();
        }
    }
}