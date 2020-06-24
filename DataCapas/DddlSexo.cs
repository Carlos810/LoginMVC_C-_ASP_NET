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
    public class DddlSexo
    {
        public DddlSexo()
        {

        }

        public DataTable tablaSexo_D()
        {
            string cadena = ConfigurationManager.ConnectionStrings["SQL"].ConnectionString;
            SqlConnection conexion = new SqlConnection(cadena);
            string sentencia = "Select Id, Nombre, Descripcion From Sexo_Persona Order by Id ASC";
            SqlDataAdapter Ada = new SqlDataAdapter(sentencia,conexion);
            DataTable dt = new DataTable();
            Ada.Fill(dt);
            return dt;
        }
    }
}
