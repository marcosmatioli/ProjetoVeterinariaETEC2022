using MySql.Data.MySqlClient;
using ProjetoClinicaNoite2508.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoClinicaNoite2508.Dados
{
    public class AcPaciente
    {
        Conexao con = new Conexao();

        public void inserirPaciente(ModelPaciente cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbPaciente (nomePaciente, enderecoPaciente, telPaciente, celPaciente) values (@nomePaciente, @enderecoPaciente, @telPaciente, @celPaciente)", con.MyConectarBD()); // @: PARAMETRO

            cmd.Parameters.Add("@nomePaciente", MySqlDbType.VarChar).Value = cm.nomePaciente;
            cmd.Parameters.Add("@enderecoPaciente", MySqlDbType.VarChar).Value = cm.enderecoPaciente;
            cmd.Parameters.Add("@telPaciente", MySqlDbType.VarChar).Value = cm.telPaciente;
            cmd.Parameters.Add("@celPaciente", MySqlDbType.VarChar).Value = cm.celPaciente;

            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }
    }
}