using Enterprise.Capas.Business;
using Enterprise.Capas.Business.EntityCapas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace crudAjax2020.Controllers
{
    public class InicioController : Controller
    {
        // GET: Inicio
        public ActionResult Index()
        {
            try
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.Now.AddDays(-1));
                Response.Cache.SetNoStore();
                if (Session["UserId"] == null)
                    throw new ApplicationException("Login Inválido.");
                //"SelectList" solo se ocupa con los DropdownList.
                ViewBag.SexoId = new SelectList(new BSexoDDL().ListaDDLSexo(), "Id", "Nombre");
                // la lista musica solo provee registros que son obtenidos mediante foreach y por tanto no se usa la propiedad "SelectList".
                ViewBag.MusicaId = new BMusicaCheckbox().ListaMusica();
                return View();
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = ex.Message;
                //"SelectList" solo se ocupa con los DropdownList.
                ViewBag.SexoId = new SelectList(new BSexoDDL().ListaDDLSexo(), "Id", "Nombre");
                // la lista musica solo provee registros que son obtenidos mediante foreach y por tanto no se usa la propiedad "SelectList".
                ViewBag.MusicaId = new BMusicaCheckbox().ListaMusica();
                return RedirectToAction("Index","Login");
            }

        }

        public ActionResult tablaPrincipal()
        {
            try
            {
                List<EPersona> list = new BPersona().ObtenerListaPersonas();
                return Json( new { hayError=false, mensaje="ok", list = list } , JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { hayError = true, mensaje = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ObtenerPersonaPorId_B(int id)
        {
            try
            {
                List<EPersona> personaConMusica = new BPersona().ObtenerPersonaPorId_B(id);
                return Json(new { hayError = false, mensaje = "ok", persona = personaConMusica }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { hayError = true, mensaje = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        
    
        [HttpPost]
        public ActionResult AgregarPersona(EPersona e)
        {
            try
            {
                /*clase evaluar expresion regular from inputs
                 * nombre , paterno, materno , sexoId
                 */ 
                string otroNombre = new BPersona().evaluarConRegex(e);

                using (TransactionScope ts = new TransactionScope())
                {
                    int idPersona = new BPersona().AgregarPersonaB(e);
                    new BPersona().AgregarMusicaB(e, idPersona);
                    ts.Complete();
                }
                List<EPersona> list = new BPersona().ObtenerListaPersonas();
                return Json(new { hayError = false, mensaje = "ok", list = list }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = ex.Message;
                return Json(new { hayError = true, mensaje = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult eliminarPersona(int id)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    new BPersona().EliminarGenerosMusicales(id);
                    
                    new BPersona().EliminarPersonaB(id);

                    ts.Complete();
                }

                List<EPersona> list = new BPersona().ObtenerListaPersonas();
                return Json( new { hayError = false, mensaje = "ok", list  }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json( new { hayError = true, mensaje = ex.Message }, JsonRequestBehavior.AllowGet );
            }
        }

        [HttpPost]
        public ActionResult editarPersona(EPersona e, int id)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    new BPersona().actualizarPersonaB(e);
                    new BPersona().EliminarGenerosMusicales(id);
                    new BPersona().AgregarMusicaB(e,id);
                    ts.Complete();
                }
                
                List<EPersona> list = new BPersona().ObtenerListaPersonas();
                return Json( new { hayError = false, mensaje = "ok", list }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = ex.Message;
                return Json(new { hayError = true, mensaje = ex.Message } , JsonRequestBehavior.AllowGet );
            }
        }





    }
}