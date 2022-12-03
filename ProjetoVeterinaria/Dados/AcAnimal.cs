using MySql.Data.MySqlClient;
using ProjetoVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjetoVeterinaria.Dados
{
    public class AcAnimal
    {
        AcConexao con = new AcConexao();

        public void cadastrarAnimal(modelAnimal cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbAnimal (nomeAnimal, fotoAnimal,codTipoAnimal, codCliente) values (@nomeAnimal, @fotoAnimal, @codTipoAnimal, @codCliente)", con.MyConectarBD()); // @: PARAMETRO

            cmd.Parameters.Add("@nomeAnimal", MySqlDbType.VarChar).Value = cm.nomeAnimal;
            cmd.Parameters.Add("@fotoAnimal", MySqlDbType.VarChar).Value = cm.fotoAnimal;
            cmd.Parameters.Add("@codTipoAnimal", MySqlDbType.VarChar).Value = cm.codTipoAnimal;
            cmd.Parameters.Add("@codCliente", MySqlDbType.VarChar).Value = cm.codCliente;


            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }
        public List<modelAnimal> GetPet(modelAnimal cm,modelCliente mod)
        {
            //pegar tudo na tabela animal
            List<modelAnimal> PetsList = new List<modelAnimal>();
            MySqlCommand cmd = new MySqlCommand("" +
                "select tbAnimal.codAnimal,tbAnimal.nomeAnimal,tbAnimal.fotoAnimal,tbAnimal.codTipoAnimal,tbAnimal.codCliente," +
                "tbCliente.nomeCliente from tbAnimal inner join tbCliente " +
                "on tbAnimal.codCliente = tbCliente.codCliente where tbAnimal.codCliente = @codCliente;", con.MyConectarBD());
            cmd.Parameters.Add("@codCliente", MySqlDbType.VarChar).Value = mod.codCliente;
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            con.MyDesConectarBD();

            //parte do cliente
            //List<modelCliente> nomeClienteList = new List<modelCliente>();
            //MySqlCommand cmd2 = new MySqlCommand("select nomeCliente from tbCliente", con.MyConectarBD());
            //MySqlDataAdapter sd2 = new MySqlDataAdapter(cmd2);
            //DataTable dt2 = new DataTable();
            //sd2.Fill(dt2);
            //con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {   
             PetsList.Add(
                    new modelAnimal
                    {
                        codAnimal = Convert.ToString(dr["codAnimal"]),
                        nomeAnimal = Convert.ToString(dr["nomeAnimal"]),
                        fotoAnimal = Convert.ToString(dr["fotoAnimal"]),
                        codCliente = Convert.ToString(dr["codCliente"]),
                        codTipoAnimal = Convert.ToString(dr["codTipoAnimal"]),
                        nomeCliente = Convert.ToString(dr["nomeCliente"])
                    }) ;
            }
            return PetsList;
        }
        public bool deleteAnimal(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbAnimal where codAnimal=@id", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@id", id);


            int i = cmd.ExecuteNonQuery();
            con.MyDesConectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool atualizarAnimal(modelAnimal cm)
        {
            MySqlCommand cmd = new MySqlCommand("update tbAnimal set nomeAnimal=@nomeAnimal,fotoAnimal=@fotoAnimal,codTipoAnimal=@codTipoAnimal,codCliente=@codCliente where codAnimal=@codAnimal", con.MyConectarBD());

            cmd.Parameters.Add("@nomeAnimal", MySqlDbType.VarChar).Value = cm.nomeAnimal;
            cmd.Parameters.Add("@fotoAnimal", MySqlDbType.VarChar).Value = cm.fotoAnimal;
            cmd.Parameters.Add("@codTipoAnimal", MySqlDbType.VarChar).Value = cm.codTipoAnimal;
            cmd.Parameters.Add("@codCliente", MySqlDbType.VarChar).Value = cm.codCliente;
            cmd.Parameters.Add("@codAnimal", MySqlDbType.VarChar).Value = cm.codAnimal;
            int i = cmd.ExecuteNonQuery();

            con.MyDesConectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public List<modelAnimal> GetAnimal()
        {
            //pegar tudo na tabela animal
            List<modelAnimal> AnimalList = new List<modelAnimal>();
            MySqlCommand cmd = new MySqlCommand("select * from tbAnimal", con.MyConectarBD());

            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sd.Fill(dt);
            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                AnimalList.Add(
                       new modelAnimal
                       {
                           codAnimal = Convert.ToString(dr["codAnimal"]),
                           nomeAnimal = Convert.ToString(dr["nomeAnimal"]),
                           fotoAnimal = Convert.ToString(dr["fotoAnimal"]),
                           codTipoAnimal = Convert.ToString(dr["codTipoAnimal"]),
                           codCliente = Convert.ToString(dr["codCliente"])
                       });
            }
            return AnimalList;
        }
    }
}