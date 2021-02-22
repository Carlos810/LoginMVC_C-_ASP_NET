using loginIn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace loginIn.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public ActionResult LoginP(loginIn.Models.loginCrudAjax data)
        {
            try
            {
                using (EjemplosEntities db = new EjemplosEntities() )
                {
                    var userDetails = db.loginCrudAjax.Where(x=>x.UserName == data.UserName && x.StringPassword == data.StringPassword).FirstOrDefault();
                    if (userDetails != null)
                    {
                        Session["userId"] = userDetails.UserId;
                    }
                    else
                    {
                        data.LoginErrorMessage = "Usuario o Contraseña Incorrectos.";
                        return View("Login",data);
                    }
                }
                return View();

            }
            catch (Exception)
            {

                throw;
            }
        }

    }





}