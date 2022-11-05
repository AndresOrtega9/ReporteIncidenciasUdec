using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportesUdec.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        [HttpGet]
        public ActionResult UnauthorizedOperation(String Operacion, String Modulo, String msjErrorExcepcion)
        {
            ViewBag.Operacion = Operacion;
            ViewBag.Modulo = Modulo;
            ViewBag.msjErrorExcepcion = msjErrorExcepcion;
            return View();
        }
    }
}