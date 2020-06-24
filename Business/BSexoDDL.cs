using Enterprise.Capas.Business.EntityCapas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.Capas.Business
{
    public class BSexoDDL
    {
        public BSexoDDL()
        {

        }

        public List<ESexo> ListaDDLSexo()
        {
            List<ESexo> list = new List<ESexo>();
            DataTable dt = new DddlSexo().tablaSexo_D();

            foreach (DataRow r in dt.Rows)
            {
                ESexo e = new ESexo();
                e.Id = Convert.ToInt32(r["Id"]);
                e.Nombre = Convert.ToString(r["Nombre"]);
                e.Descripcion = Convert.ToString(r["Descripcion"]);
                list.Add(e);
            }
            return list;
        }

    }
}
