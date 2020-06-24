using Enterprise.Capas.Business.EntityCapas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.Capas.Business
{
    public class BMusicaCheckbox
    {
        public BMusicaCheckbox()
        {

        }

        public List<EMusica> ListaMusica()
        {
            List<EMusica> list = new List<EMusica>();
            DataTable dt = new DMusicaCheckbox().tablaMusica_D();

            foreach (DataRow r in dt.Rows)
            {
                EMusica e = new EMusica();
                e.Id = Convert.ToInt32(r["Id"]);
                e.Nombre = Convert.ToString(r["Nombre"]);
                e.Descripcion = Convert.ToString(r["Descripcion"]);
                list.Add(e);
            }
            return list;
        }
    }
}
