using MySql.Data.MySqlClient;
using ProjetoVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace ProjetoVeterinaria.Dados
{
    public class acVeterinario
    {

        AcConexao con = new AcConexao();

        public void cadastrarVeterinario(modelVeterinario cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbVeterinario (nomeVet) values (@nomeVet)", con.MyConectarBD()); // @: PARAMETRO

            cmd.Parameters.Add("@nomeVet", MySqlDbType.VarChar).Value = cm.nomeVet;

            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }
    }
}