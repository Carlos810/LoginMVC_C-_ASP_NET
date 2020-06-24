using Enterprise.Capas.Business;
using Enterprise.Capas.Business.EntityCapas;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Enterprise.Capas.Business
{
    public class BPersona
    {
        public BPersona()
        {

        }

        public List<EPersona> ObtenerListaPersonas()
        {
            List<EPersona> listP = new List<EPersona>();
            DataTable dt = new DPersona().ObtenerDataTable();

            EPersona e = new EPersona();
            int idPersonaFor = 0;
            foreach (DataRow r in dt.Rows)
            {
                if (idPersonaFor != Convert.ToInt32(r["V_Id"])) //Es Nueva persona.
                {
                    //Tabla Persona.
                    e = new EPersona();
                    e.Id = Convert.ToInt32(r["V_Id"]);
                    e.Nombre = Convert.ToString(r["V_Nomb"]);
                    e.Paterno = Convert.ToString(r["V_Pate"]);
                    e.Materno = Convert.ToString(r["V_Mate"]);
                    e.SexoId = Convert.ToInt32(r["V_SexoId"]);
                    //Tabla Sexo.
                    e.SEXO.Id = Convert.ToInt32(r["S_Id"]);
                    e.SEXO.Nombre = Convert.ToString(r["S_Nomb"]);
                    e.SEXO.Descripcion = Convert.ToString(r["S_Desc"]);

                    e.Count = 0;

                    if (System.DBNull.Value != r["MP_Id"])
                    {

                        EMusica m = new EMusica();
                        m.Id = Convert.ToInt32(r["MP_Id"]);
                        m.Nombre = Convert.ToString(r["MP_Nomb"]);
                        m.Descripcion = Convert.ToString(r["MP_Desc"]);
                        e.ListaMusica.Add(m);
                    }

                    listP.Add(e);

                }
                else //Es la misma Persona
                {
                    if (System.DBNull.Value != r["MP_Id"])
                    {
                        EMusica m = new EMusica();
                        m.Id = Convert.ToInt32(r["MP_Id"]);
                        m.Nombre = Convert.ToString(r["MP_Nomb"]);
                        m.Descripcion = Convert.ToString(r["MP_Desc"]);
                        e.ListaMusica.Add(m);
                    }
                }

                idPersonaFor = Convert.ToInt32(r["V_Id"]);
            }

            return listP;
        }

        public List<EPersona> ObtenerPersonaPorId_B(int id)
        {
            DataTable dt = new DPersona().obtenerRegistroUnaPersona_D(id);
            List<EPersona> listaM = new List<EPersona>();
            EPersona e = new EPersona();

            int idPersonaFor = 0;
            foreach (DataRow r in dt.Rows)
            {
                if (idPersonaFor != Convert.ToInt32(r["V_Id"])) //Es Nueva persona.
                {
                    //Tabla Persona.
                    e = new EPersona();
                    e.Id = Convert.ToInt32(r["V_Id"]);
                    e.Nombre = Convert.ToString(r["V_Nomb"]);
                    e.Paterno = Convert.ToString(r["V_Pate"]);
                    e.Materno = Convert.ToString(r["V_Mate"]);
                    e.SexoId = Convert.ToInt32(r["V_SexoId"]);
                    //Tabla Sexo.
                    e.SEXO.Id = Convert.ToInt32(r["S_Id"]);
                    e.SEXO.Nombre = Convert.ToString(r["S_Nomb"]);
                    e.SEXO.Descripcion = Convert.ToString(r["S_Desc"]);

                    //e.Count = 0;

                    if (System.DBNull.Value != r["MP_Id"]) //obtener musica favorita
                    {

                        EMusica m = new EMusica();
                        m.Id = Convert.ToInt32(r["MP_Id"]);
                        m.Nombre = Convert.ToString(r["MP_Nomb"]);
                        m.Descripcion = Convert.ToString(r["MP_Desc"]);
                        e.ListaMusica.Add(m);
                    }

                    listaM.Add(e);

                }
                else //Es la misma Persona y solo se agregan sus preferencias musicales.
                {
                    if (System.DBNull.Value != r["MP_Id"])
                    {
                        EMusica m = new EMusica();
                        m.Id = Convert.ToInt32(r["MP_Id"]);
                        m.Nombre = Convert.ToString(r["MP_Nomb"]);
                        m.Descripcion = Convert.ToString(r["MP_Desc"]);
                        e.ListaMusica.Add(m);
                    }
                }

                idPersonaFor = Convert.ToInt32(r["V_Id"]);
            }


            return listaM;
        }

        public int AgregarPersonaB(EPersona e)
        {
            int[] numEnteros = new DPersona().AgregarPersonaD(e);

            if (numEnteros[0] != 1)
            {
                throw new ApplicationException("La persona no se Agregó correctamente.");
            }

            return numEnteros[1];
        }

        public void AgregarMusicaB(EPersona e, int idPersona)
        {            
            List<string> lmusica = new List<string>();
            lmusica.Add(e.chk1);
            lmusica.Add(e.chk2);
            lmusica.Add(e.chk4);
            lmusica.Add(e.chk5);
            
            foreach (string music in lmusica) // ciclo para extraer el id de los generos musicales que fueron tildados.
            {
                if (music.Contains("false")){
                    continue;
                }else{  // filtrar elementos que contengan "true".

                    // limpiar cadena de texto y transformar "idMusicaString" a int32.
                    string idMusicaString = music.Replace("true", "").TrimEnd();
                    int idMusicaInt = Convert.ToInt32(idMusicaString);

                    int filasAfectadas = new DPersona().AgregarMusicaD(idMusicaInt, idPersona);
                    if (filasAfectadas != 1){
                        throw new ApplicationException("El genero Musical no se Agregó correctamente.");
                    }
                }
            }
        }

        public void EliminarPersonaB(int id)
        {
            int filasAfectadas = new DPersona().eliminarPersonaD(id);
            if (filasAfectadas != 1)
            {
                throw new ApplicationException("No se eliminó la persona seleccionada.");
            }
        }

        public void EliminarGenerosMusicales(int id)
        {
            int filasAfectadas = new DPersona().eliminarGenerosMusicalesD(id);
            if (filasAfectadas <0 && filasAfectadas >4 )
            {
                throw new ApplicationException("Error al intentar eliminar los generos musicales");
            }
        }

        public void actualizarPersonaB(EPersona e)
        {
            int filasAfectadas = new DPersona().actualizarPersonaD(e);
            if (filasAfectadas != 1)
            {
                throw new ApplicationException("La persona no fue actualizada correctamente");
            }
        }

        public string evaluarConRegex(EPersona e)
        {
            string message = "";

            Regex rxName = new Regex("^[a-z ,.'-]+$/i");


            if( rxName.IsMatch(e.Nombre) ){
                //continue whit next input. 
            }else{
                //show a message to fill the current input.
                message = "Ingresa un tipo de dato valido.";
            }

            return message;
        }

    }



}

