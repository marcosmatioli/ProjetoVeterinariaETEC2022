using MySql.Data.MySqlClient;
using ProjetoVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjetoVeterinaria.Dados
{
    public class acTipoAnimal
    {
        AcConexao con = new AcConexao();

        public void cadastrarTipoAnimal(modelTipoAnimal cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbTipoAnimal (tipoAnimal) values (@tipoAnimal)", con.MyConectarBD()); // @: PARAMETRO

            cmd.Parameters.Add("@tipoAnimal", MySqlDbType.VarChar).Value = cm.tipoAnimal;

            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }
        public List<modelTipoAnimal> GetTipoAnimal()
        {
            List<modelTipoAnimal> TipoAnimalList = new List<modelTipoAnimal>();

            MySqlCommand cmd = new MySqlCommand("select * from tbTipoAnimal", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                TipoAnimalList.Add(
                    new modelTipoAnimal
                    {
                        codTipoAnimal = Convert.ToString(dr["codTipoAnimal"]),
                        tipoAnimal = Convert.ToString(dr["tipoAnimal"]),
                    });
            }
            return TipoAnimalList;
        }
        public bool DeleteTipoAnimal(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbTipoAnimal where codTipoAnimal=@id", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@id", id);


            int i = cmd.ExecuteNonQuery();
            con.MyDesConectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }

        public bool atualizarTipoAnimal(modelTipoAnimal cm)
        {
            MySqlCommand cmd = new MySqlCommand("update tbTipoAnimal set tipoAnimal=@tipoAnimal where codTipoAnimal=@codTipoAnimal", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@tipoAnimal", cm.tipoAnimal);
            cmd.Parameters.AddWithValue("@codTipoAnimald", cm.codTipoAnimal);

            int i = cmd.ExecuteNonQuery();
            con.MyDesConectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}