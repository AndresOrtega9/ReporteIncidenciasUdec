using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportesUdec.Controllers
{
    public class CerrarSesionController : Controller
    {
        // GET: CerrarSesion
        public ActionResult Cerrar()
        {
            Session["user"] = null;
            return RedirectToAction("Login", "Acceso");
        }
    }
}