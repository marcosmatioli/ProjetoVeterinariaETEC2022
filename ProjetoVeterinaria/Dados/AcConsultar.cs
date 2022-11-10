﻿using MySql.Data.MySqlClient;
using ProjetoVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjetoVeterinaria.Dados
{
    public class AcConsultar
    {
        AcConexao con = new AcConexao();
        public DataTable ConsultaTable(modelConsultar mod)
        {

            MySqlCommand cmd = new MySqlCommand("select * from " + mod.Tablenome + "", con.MyConectarBD());
            //cmd.Parameters.Add("@Tablenome", MySqlDbType.VarString).Value = mod.Tablenome;
            cmd.CommandType = CommandType.Text; // comando para visualizar a view criada no MySql
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable tabela = new DataTable();
            da.Fill(tabela);
            con.MyDesConectarBD();
            return tabela;
        }
    }
}