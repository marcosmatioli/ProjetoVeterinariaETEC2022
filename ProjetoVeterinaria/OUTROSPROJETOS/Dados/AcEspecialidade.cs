using MySql.Data.MySqlClient;
using ProjetoClinicaNoite2508.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProjetoClinicaNoite2508.Dados
{
    public class AcEspecialidade
    {
        Conexao con = new Conexao();

        public void inserirEspecialidade(ModelEspecialidade cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbEspecialidade (Especialidade) values (@Especialidade)", con.MyConectarBD()); // @: PARAMETRO

            cmd.Parameters.Add("@Especialidade", MySqlDbType.VarChar).Value = cm.Especialidade;

            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }
    }
}