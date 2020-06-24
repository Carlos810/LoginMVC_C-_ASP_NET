using Enterprise.Capas.Business.EntityCapas;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.Capas.Business
{
    public class DPersona
    {
        public DPersona()
        {

        }

        public DataTable ObtenerDataTable()
        {
            string cadena = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString;
            SqlConnection conexion = new SqlConnection(cadena);
            SqlCommand comand = new SqlCommand("sp_SelectTablesFull_CrudAjax2020", conexion);
            comand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter Ada = new SqlDataAdapter(comand);
            DataTable dt = new DataTable();
            Ada.Fill(dt);
            return dt;
        }

        public DataTable obtenerRegistroUnaPersona_D(int id)
        {
            string cadena = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString;
            SqlConnection conexion = new SqlConnection(cadena);
            SqlCommand comand = new SqlCommand("sp_selectById_CrudAjax2020", conexion);
            comand.CommandType = CommandType.StoredProcedure;
            comand.Parameters.Add(new SqlParameter() { ParameterName= "@Id", SqlDbType = SqlDbType.Int, Value= id });
            SqlDataAdapter Ada = new SqlDataAdapter(comand);
            DataTable dt = new DataTable();
            Ada.Fill(dt);           
            return dt;
        }

        public int[] AgregarPersonaD(EPersona e)
        {
            string cadena = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString;
            SqlConnection conexion = new SqlConnection(cadena);
            SqlCommand comand = new SqlCommand("sp_InsertPersona_Vista_Persona_SolCrudAjax2020", conexion);
            comand.CommandType = CommandType.StoredProcedure;
            //parametros
            comand.Parameters.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = SqlDbType.VarChar, Size=50, Value= e.Nombre });
            comand.Parameters.Add(new SqlParameter { ParameterName = "@Paterno", SqlDbType = SqlDbType.VarChar, Size = 50, Value = e.Paterno });
            comand.Parameters.Add(new SqlParameter { ParameterName = "@Materno", SqlDbType = SqlDbType.VarChar, Size = 50, Value = e.Materno });
            comand.Parameters.Add(new SqlParameter { ParameterName = "@SexoId", SqlDbType = SqlDbType.Int, Value = e.SexoId });
            comand.Parameters.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Direction = ParameterDirection.Output });

            conexion.Open();
            int filasAfectadas = comand.ExecuteNonQuery();
            int IdPersonaAgregada = Convert.ToInt32(comand.Parameters["@Id"].Value);
            conexion.Close();

            int[] numEnteros = { filasAfectadas, IdPersonaAgregada };
            return numEnteros;
        }

        public int AgregarMusicaD( int idMusic , int idPersona )
        {
            string cadena = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString;
            SqlConnection conexion = new SqlConnection(cadena);
            SqlCommand comand = new SqlCommand("sp_InsertGeneroMusica_Puente_Musica_Persona_SolCrudAjax2002", conexion);
            comand.CommandType = CommandType.StoredProcedure;
            //parametros
            comand.Parameters.Add(new SqlParameter { ParameterName = "@PersonaId", SqlDbType = SqlDbType.Int, Value = idPersona });
            comand.Parameters.Add(new SqlParameter { ParameterName = "@MusicaId", SqlDbType = SqlDbType.Int, Value = idMusic });

            conexion.Open();
            int filasAfectadas = comand.ExecuteNonQuery();
            conexion.Close();
            return filasAfectadas;
        }

        public int eliminarPersonaD(int id)
        {
            string cadena = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString;
            SqlConnection conexion = new SqlConnection(cadena);
            SqlCommand comand = new SqlCommand("sp_DeletePersona_Vista_Persona_SolCrudAjax", conexion);
            comand.CommandType = CommandType.StoredProcedure;
            //parametro
            comand.Parameters.Add( new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id });
            conexion.Open();
            int filasAfectadas = comand.ExecuteNonQuery();
            conexion.Close();
            return filasAfectadas;
        }

        public int eliminarGenerosMusicalesD(int id)
        {
            string cadena = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString;
            SqlConnection conexion = new SqlConnection(cadena);
            SqlCommand comand = new SqlCommand("sp_DeleteGenerosMusicales_Puente_Musica_Persona_SolCrudAjax2020", conexion);
            comand.CommandType = CommandType.StoredProcedure;
            //parametro
            comand.Parameters.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id });
            conexion.Open();
            int filasAfectadas = comand.ExecuteNonQuery();
            conexion.Close();
            return filasAfectadas;
        }

        public int actualizarPersonaD(EPersona e)
        {
            string cadena = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString;
            SqlConnection conexion = new SqlConnection(cadena);
            SqlCommand comand = new SqlCommand("sp_UpdatePersona_Vista_Persona_SolCrudAjas2020", conexion);
            comand.CommandType = CommandType.StoredProcedure;
            //params
            comand.Parameters.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = SqlDbType.VarChar, Size = 50, Value =  e.Nombre } );
            comand.Parameters.Add(new SqlParameter { ParameterName = "@Paterno", SqlDbType = SqlDbType.VarChar, Size = 50, Value = e.Paterno });
            comand.Parameters.Add(new SqlParameter { ParameterName = "@Materno", SqlDbType = SqlDbType.VarChar, Size = 50, Value = e.Materno });
            comand.Parameters.Add(new SqlParameter { ParameterName = "@SexoId", SqlDbType = SqlDbType.Int, Value = e.SexoId });
            comand.Parameters.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = e.Id});
        
            conexion.Open();
            int filasAfectadas = comand.ExecuteNonQuery();
            conexion.Close();
            return filasAfectadas;
        }

    }
}
