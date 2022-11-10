using MySql.Data.MySqlClient;
using ProjetoClinicaNoite2508.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjetoClinicaNoite2508.Dados
{
    public class AcConsultar
    {

        Conexao con = new Conexao();
        public DataTable ConsultaTable(ModelConsultar mod)
           {

            MySqlCommand cmd = new MySqlCommand("select * from "+mod.Tablenome+"", con.MyConectarBD());
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