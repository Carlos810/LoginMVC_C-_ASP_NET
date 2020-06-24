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
    public class DMusicaCheckbox
    {
        public DMusicaCheckbox()
        {

        }

        public DataTable tablaMusica_D()
        {
            string cadena = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString;
            SqlConnection conexion = new SqlConnection(cadena);
            string sentencia = "Select Id, Nombre, Descripcion From Musica_Persona ";
            SqlDataAdapter Ada = new SqlDataAdapter(sentencia, conexion);
            DataTable dt = new DataTable();
            Ada.Fill(dt);
            return dt;
        }
    }
}
