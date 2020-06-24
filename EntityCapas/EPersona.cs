using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Enterprise.Capas.Business.EntityCapas
{
    public class EPersona
    {
        public EPersona()
        {

        }


        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Paterno { get; set; }

        public string Materno { get; set; }

        public int SexoId { get; set; }

        public int Count { get; set; }

        public string chk1 { get; set; }
        public string chk2 { get; set; }
        public string chk4 { get; set; }
        public string chk5 { get; set; }



        //Objeto autogenerado de LLave Foranea

        private ESexo sexo;

        public ESexo SEXO
        {
            get {
                    if (sexo == null)
                    {
                        sexo = new ESexo();
                    } 
                    return sexo;
                }
            set {
                    if (sexo == null)
                    {
                    sexo = new ESexo();
                    }
                    sexo = value;
                }
        }

        private EMusica musica;

        public EMusica MUSICA
        {
            get {
                    if (musica == null)
                        musica = new EMusica();
                        return musica;
                }
            set {
                    if (musica == null)
                        musica = new EMusica();
                        musica = value;
                }
        }

        //Lista de Musica Favorita

        private List<EMusica> listamusica;

        public List<EMusica> ListaMusica
        {
            get {
                    if (listamusica == null)
                        listamusica = new List<EMusica>();
                    return listamusica;
                }
            set {
                    if (listamusica == null)
                        listamusica = new List<EMusica>();
                    listamusica = value;
                    }
        }


    }
}
