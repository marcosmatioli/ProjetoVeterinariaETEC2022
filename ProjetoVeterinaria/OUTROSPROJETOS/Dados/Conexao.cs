using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Drawing;


namespace ProjetoClinicaNoite2508.Dados
{
    public class Conexao
    {
        MySqlConnection cn = new MySqlConnection("Server=localhost; DataBase=bdClinica; User=root;pwd=rootroot1995.M");
        public static string msg;

        public MySqlConnection MyConectarBD() //Método: MyConectarBD()
        {
            try
            {
                cn.Open();
            }

            catch (Exception erro)
            {
                msg = "Ocorreu um erro ao se conectar" + erro.Message;
            }
            return cn;
        }


        public MySqlConnection MyDesConectarBD()  //Método: MyDesConectarBD()
        {

            try
            {
                cn.Close();
            }

            catch (Exception erro)
            {
                msg = "Ocorreu um erro ao se desconectar" + erro.Message;
            }
            return cn;
        }
    }
}