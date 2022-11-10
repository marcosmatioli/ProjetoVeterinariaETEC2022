using MySql.Data.MySqlClient;
using ProjetoClinicaNoite2508.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoClinicaNoite2508.Dados
{
    public class AcMedico
    {
        Conexao con = new Conexao();

        public void inserirMedico(ModelMedico cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbMedico (nomeMedico, codEspecialidade) values (@nomeMedico, @codEspecialidade)", con.MyConectarBD()); // @: PARAMETRO

            cmd.Parameters.Add("@nomeMedico", MySqlDbType.VarChar).Value = cm.nomeMedico;
            cmd.Parameters.Add("@codEspecialidade", MySqlDbType.VarChar).Value = cm.codEspecialidade;

            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }
    }
}