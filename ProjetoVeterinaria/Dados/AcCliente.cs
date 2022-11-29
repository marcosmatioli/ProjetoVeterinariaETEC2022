using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;
using ProjetoVeterinaria.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ProjetoVeterinaria.Dados
{
    public class AcCliente
    {
        //classe para cadastrar cliente no banco de dados
        AcConexao con = new AcConexao();

        public void cadastrarCliente(modelCliente cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbCliente (nomeCliente, telefoneCliente, enderecoCliente, cepCliente, usuario, senha, tipoUsuario) values (@nomeCliente, @telefoneCliente, @enderecoCliente,@cepCliente, @usuario, @senha, @TipoUsuario)", con.MyConectarBD()); // @: PARAMETRO
            
            cmd.Parameters.Add("@nomeCliente", MySqlDbType.VarChar).Value = cm.nomeCliente;
            cmd.Parameters.Add("@telefoneCliente", MySqlDbType.VarChar).Value = cm.telefoneCliente;
            cmd.Parameters.Add("@cepCliente", MySqlDbType.VarChar).Value = cm.cepCliente;
            cmd.Parameters.Add("@enderecoCliente", MySqlDbType.VarChar).Value = cm.enderecoCliente;
            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = cm.usuario;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = cm.senha;
            cmd.Parameters.Add("@TipoUsuario", MySqlDbType.VarChar).Value = cm.usuariopadrao;
            //fiz um cm.usuariopadrao para todos que criarem conta no site sejam usuarios normais e usuarios ADM
            // tem que contactar a equipe para criação de novos usuarios ADM para a segurança do sistema

            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }
        //o cadastrar ADM, o adm pode escolher se a pessoa cadastrada é um cliente ou outro veterinario que vai poder usar o sistema
        public void cadastrarClienteADM(modelCliente cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tbCliente (nomeCliente, telefoneCliente, enderecoCliente, cepCliente, usuario, senha, tipoUsuario) values (@nomeCliente, @telefoneCliente, @enderecoCliente,@cepCliente, @usuario, @senha, @TipoUsuario)", con.MyConectarBD()); // @: PARAMETRO

            cmd.Parameters.Add("@nomeCliente", MySqlDbType.VarChar).Value = cm.nomeCliente;
            cmd.Parameters.Add("@telefoneCliente", MySqlDbType.VarChar).Value = cm.telefoneCliente;
            cmd.Parameters.Add("@cepCliente", MySqlDbType.VarChar).Value = cm.cepCliente;
            cmd.Parameters.Add("@enderecoCliente", MySqlDbType.VarChar).Value = cm.enderecoCliente;
            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = cm.usuario;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = cm.senha;
            cmd.Parameters.Add("@TipoUsuario", MySqlDbType.VarChar).Value = cm.TipoUsuario;

            cmd.ExecuteNonQuery();
            con.MyDesConectarBD();
        }
        //testar se tem o usuario
        public void TestarUsuario(modelCliente mod)
        {
            MySqlCommand cmd = new MySqlCommand("select usuario,senha,tipoUsuario from tbCliente where usuario = @usuario and senha = @senha", con.MyConectarBD());

            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = mod.usuario;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = mod.senha;

            MySqlDataReader leitor;
            
            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    mod.usuario = Convert.ToString(leitor["usuario"]);
                    mod.senha = Convert.ToString(leitor["senha"]);
                    mod.TipoUsuario = Convert.ToInt16(leitor["tipoUsuario"]);
                }
            }
            else
            {
                mod.usuario = null;
                mod.senha = null;
                mod.TipoUsuario = 0;
            }
            con.MyDesConectarBD();
        }
        public string GetcodCliente(modelCliente mod)
        {
            MySqlCommand cmd = new MySqlCommand("select codCliente from tbCliente where usuario = @usuario and senha = @senha", con.MyConectarBD());

            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = mod.usuario;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = mod.senha;

            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    mod.codCliente= Convert.ToString(leitor["codCliente"]);
                }
            }
            else
            {
               mod.codCliente = null;
            }
            con.MyDesConectarBD();

            return mod.codCliente;
        }
        public List<modelAnimal> GetAnimalUsuario(modelCliente mod, string codClienteControle)
        {
            List<modelAnimal> AnimalUsuarioList = new List<modelAnimal>();

            MySqlCommand cmd = new MySqlCommand("" +
                "select tbAnimal.codAnimal,tbAnimal.nomeAnimal,tbAnimal.fotoAnimal,tbAnimal.codTipoAnimal,tbAnimal.codCliente,tbCliente.nomeCliente " +
                "from tbAnimal inner join tbCliente on tbAnimal.codCliente = tbCliente.codCliente " +
                "where tbCliente.codCliente = @codCliente", con.MyConectarBD());
            //o problema é que eu preciso trazer um dado de alguma forma para colocar aqui
            // codCliente ou nomeCliente para entrar na comparação
            cmd.Parameters.Add("@codCliente", MySqlDbType.VarChar).Value = codClienteControle;
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesConectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                AnimalUsuarioList.Add(
                    new modelAnimal
                    {
                        codAnimal = Convert.ToString(dr["codAnimal"]),
                        nomeAnimal = Convert.ToString(dr["nomeAnimal"]),
                        fotoAnimal = Convert.ToString(dr["fotoAnimal"]),
                        codTipoAnimal = Convert.ToString(dr["codTipoAnimal"]),
                        codCliente = Convert.ToString(dr["codCliente"]),
                        nomeCliente = Convert.ToString(dr["nomeCliente"])
                    });
            }
            return AnimalUsuarioList;
        }
        public bool DeleteAnimal(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tbAnimal where codAnimal=@id", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@id", id);


            int i = cmd.ExecuteNonQuery();
            con.MyDesConectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        } // arrumar ainda

        public bool atualizarAnimal(modelAnimal cm)
        {
            MySqlCommand cmd = new MySqlCommand("update tbAnimal set tipoAnimal=@tipoAnimal where codTipoAnimal=@codTipoAnimal", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@tipoAnimal", cm.codAnimal);
            cmd.Parameters.AddWithValue("@codTipoAnimald", cm.codTipoAnimal);

            int i = cmd.ExecuteNonQuery();
            con.MyDesConectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        } // arrumar ainda
        public bool editarCliente(modelCliente cm)
        {
            MySqlCommand cmd = new MySqlCommand("update tbCliente set nomeCliente=@nomeCliente,telefoneCliente=@telefoneCliente,cepCliente=@cepCliente," +
                "enderecoCliente=@enderecoCliente,usuario=@usuario,senha=@senha,TipoUsuario=@TipoUsuario where codCliente=@codCliente", con.MyConectarBD());

            cmd.Parameters.Add("@nomeCliente", MySqlDbType.VarChar).Value = cm.nomeCliente;
            cmd.Parameters.Add("@telefoneCliente", MySqlDbType.VarChar).Value = cm.telefoneCliente;
            cmd.Parameters.Add("@cepCliente", MySqlDbType.VarChar).Value = cm.cepCliente;
            cmd.Parameters.Add("@enderecoCliente", MySqlDbType.VarChar).Value = cm.enderecoCliente;
            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = cm.usuario;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = cm.senha;
            cmd.Parameters.Add("@TipoUsuario", MySqlDbType.VarChar).Value = cm.usuariopadrao;
            cmd.Parameters.Add("@codCliente", MySqlDbType.VarChar).Value = cm.codCliente;
            int i = cmd.ExecuteNonQuery();

            con.MyDesConectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}