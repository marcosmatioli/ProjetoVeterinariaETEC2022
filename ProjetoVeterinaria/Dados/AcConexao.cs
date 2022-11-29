using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using MySql.Data.MySqlClient;

namespace ProjetoVeterinaria.Dados
{
    public class AcConexao
    {
        MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bdClinicaVeterinaria; User=root;pwd=rootroot1995.M");
        //MySqlConnection con = new MySqlConnection("Server=localhost; DataBase=bdClinicaVeterinaria; User=root;pwd=12345678");
        public static string msg;

        public MySqlConnection MyConectarBD() //Método: MyConectarBD()
        {
            try
            {
                con.Open();
            }

            catch (Exception erro)
            {
                msg = "Ocorreu um erro ao se conectar" + erro.Message;
            }
            return con;
        }


        public MySqlConnection MyDesConectarBD()  //Método: MyDesConectarBD()
        {
            try
            {
                con.Close();
            }

            catch (Exception erro)
            {
                msg = "Ocorreu um erro ao se desconectar" + erro.Message;
            }
            return con;
        }
    }
}