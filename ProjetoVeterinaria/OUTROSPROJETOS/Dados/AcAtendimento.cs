using MySql.Data.MySqlClient;
using ProjetoClinicaNoite2508.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoClinicaNoite2508.Dados
{
    public class AcAtendimento
    {
            Conexao con = new Conexao();

            public void inserirAtendimento(ModelAtendimento cm)
            {
                MySqlCommand cmd = new MySqlCommand("insert into tbAtendimento " +
                    "(dataAtendimento, codPaciente, codMedico, Diagnostico, horaAtendimento) " +
                    "values (@dataAtendimento, @codPaciente, @codMedico, @Diagnostico, @horaAtendimento)", con.MyConectarBD()); // @: PARAMETRO

                cmd.Parameters.Add("@dataAtendimento", MySqlDbType.VarChar).Value = cm.dataAtendimento;
                cmd.Parameters.Add("@codPaciente", MySqlDbType.VarChar).Value = cm.codPaciente;
                cmd.Parameters.Add("@codMedico", MySqlDbType.VarChar).Value = cm.codMedico;
                cmd.Parameters.Add("@Diagnostico", MySqlDbType.VarChar).Value = cm.Diagnostico;
                cmd.Parameters.Add("@horaAtendimento", MySqlDbType.VarChar).Value = cm.horaAtendimento;

                cmd.ExecuteNonQuery();
                con.MyDesConectarBD();
            }
        public void TemConsulta(ModelAtendimento mod)
        {
            MySqlCommand cmd = new MySqlCommand("select * from tbAtendimento where dataAtendimento = @dataAtendimento", con.MyConectarBD());
            cmd.Parameters.Add("@dataAtendimento", MySqlDbType.VarChar).Value = mod.dataAtendimento;
            //cmd.Parameters.Add("@hora", MySqlDbType.VarChar).Value = agenda.horaAgen;
            MySqlDataReader leitor;
            leitor = cmd.ExecuteReader();
            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    mod.TemConsulta = false;
                    // se ele encontrar um igual ele coloca false então não é para agendar
                }
            }
            else
            {
                mod.TemConsulta = true;
                //se for verdade é pq ele não encontrou nenhum caso no banco então pode agendar
            }
            con.MyDesConectarBD();
        }
    }
}