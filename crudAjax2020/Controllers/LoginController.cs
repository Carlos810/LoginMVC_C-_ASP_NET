using crudAjax2020.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace crudAjax2020.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {   
            return View();
        }

        [HttpPost]
        public ActionResult Index(crudAjax2020.Models.loginCrudAjax data)
        {
            try
            {
                using (EjemplosEntities db = new EjemplosEntities())
                {
                    var userDetails = db.loginCrudAjax.Where(x => x.UserName == data.UserName && x.StringPassword == data.StringPassword).FirstOrDefault();
                    if (userDetails == null)
                    {
                        data.LoginErrorMessage = "Usuario o Constraseña incorrectos.";
                        return View("Index", data);
                    }
                    else
                    {
                        Session["UserId"] = data.UserId;
                        Session["UserName"] = data.UserName;
                        return RedirectToAction("Index", "Inicio");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = ex.Message;
                return View("Index");
            }
        }

        public void Logout()
        {           
            //int UserId = (int)Session["UserId"];
            Session.Abandon();
            Session.RemoveAll();
            Session.Clear();
            Response.Redirect("Index",true);
            //return RedirectToAction("Index","Login");
        }



    }
}