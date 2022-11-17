using ReportesUdec.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReportesUdec.DbOp;
using ReportesUdec.Models.ViewModels;
using ReportesUdec.Filtros;
using System.Web.UI;
using System.Threading;
using System.Globalization;
using Rotativa;

namespace ReportesUdec.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        [Autorizacion(idOperacion: 1)]
        public ActionResult Administrador()
        {
            return View();
        }

        [Autorizacion(idOperacion: 2)]
        public ActionResult PersonalAseo()
        {
            return View();
        }

        public ActionResult PersonalAseoVerReportes()
        {
            using (ReportesUdec_dbEntities model = new ReportesUdec_dbEntities())
            {
                return View(model.Reporte.ToList());
            }

        }
        [Autorizacion(idOperacion: 3)]
        public ActionResult PersonalReparacion()
        {
            return View();
        }

        public ActionResult PersonalReparacionVerReportes()
        {
            using (ReportesUdec_dbEntities model = new ReportesUdec_dbEntities())
            {
                return View(model.Reporte.ToList());
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult AdministradorVerPersonal()
        {
            using (ReportesUdec_dbEntities model = new ReportesUdec_dbEntities())
            {
                return View(model.Usuario.ToList());
            }
        }

        public ActionResult AdministradorAgregarPersonal()
        {
            return View();
        }

        public ActionResult AdministradorVerReportes()
        {
            using (ReportesUdec_dbEntities model = new ReportesUdec_dbEntities())
            {
                return View(model.Reporte.ToList());
            }
        }

        public ActionResult ReporteAdmin()
        {
            return new ActionAsPdf("PersonalReparacionVerReportes", new { rpt = "Reporte generado" })
            { FileName = "reporte_administrador.pdf" };
        }

        MngReporte mngRep = new MngReporte();
        DatosDDl datos = new DatosDDl();
        public ActionResult Nuevo()
        {
            return View();
        }

        [HttpPost]
        public void Nuevo(Usuario model, FormCollection Form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var _db = new ReportesUdec_dbEntities())
                    {
                        var DateAndTime = DateTime.Now;
                        var fecha = DateAndTime.Date.ToString("yyyy-MM-dd");

                        var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                        var Charsarr = new char[8];
                        var random = new Random();

                        for (int i = 0; i < Charsarr.Length; i++)
                        {
                            Charsarr[i] = characters[random.Next(characters.Length)];
                        }

                        var resultString = new String(Charsarr);
                        

                        Usuario rm = new Usuario()
                        {
                            Fecha = fecha,
                            Nombre = model.Nombre,
                            Correo = model.Correo,
                            Contraseña = resultString,
                            Rol_Id = int.Parse(Form["ddlRoles"]),

                        };
                        mngRep.Nuevo(rm);
                        Response.Redirect("Administrador");
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public ActionResult EditarUsuariosAdmin(Usuario model, int id)
        {
            using (ReportesUdec_dbEntities db = new ReportesUdec_dbEntities())
            {
                var oUsuario = db.Usuario.Find(id);
                model.Nombre = oUsuario.Nombre;
                model.Correo = oUsuario.Correo;
                model.Rol_Id =oUsuario.Rol_Id;
                model.Usuario_Id = oUsuario.Usuario_Id;
            }
            return View(model);          
        }

        [HttpPost]
        public ActionResult EditarUsuariosAdmin(Usuario model, FormCollection Form)
        {          
                    using (var _db = new ReportesUdec_dbEntities())
                    {
                        var DateAndTime = DateTime.Now;
                        var fecha = DateAndTime.Date.ToString("yyyy-MM-dd");

                        var oUsuario = _db.Usuario.Find(model.Usuario_Id);
                        oUsuario.Nombre = model.Nombre;
                        oUsuario.Correo = model.Correo;
                        oUsuario.Rol_Id = int.Parse(Form["ddlRoles"]);
                        oUsuario.Usuario_Id = model.Usuario_Id;


                        _db.Entry(oUsuario).State = System.Data.Entity.EntityState.Modified;
                        _db.SaveChanges();               
            };
            string msj = "!Se han editado datos del usuario!";
            ViewBag.msj = msj;
            return View(model);
        }

        [HttpGet]
        public ActionResult Eliminar(int id)
        {
            using (ReportesUdec_dbEntities db = new ReportesUdec_dbEntities())
            {
                var oUsuario = db.Usuario.Find(id);
                db.Usuario.Remove(oUsuario);
                db.SaveChanges();
            }
            return Redirect("Administrdor");
        }

        //EDITAR REPORTES
        public ActionResult EditarReportesPersonal(Reporte model, int id)//a
        {
            using (ReportesUdec_dbEntities db = new ReportesUdec_dbEntities())
            {
                var oReporte = db.Reporte.Find(id);
                //model.Ruta_Imagen = oReporte.Ruta_Imagen;
                model.Estado = oReporte.Estado;
                model.Reporte_Id = oReporte.Reporte_Id;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditarReportesPersonal(Reporte model)
        {
            //string filename = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
            //string ext = Path.GetExtension(model.ImageFile.FileName);
            //filename = filename + DateTime.Now.ToString("yymmssfff") + ext;
            //model.Ruta_Imagen = "~/Imagenes/" + filename;
            //filename = Path.Combine(Server.MapPath("~/Imagenes/"), filename);
            //model.ImageFile.SaveAs(filename);

            using (var _db = new ReportesUdec_dbEntities())
            {
                var oReporte = _db.Reporte.Find(model.Reporte_Id);
                //oReporte.Ruta_Imagen = model.Ruta_Imagen;
                oReporte.Estado = "Reparado";

                _db.Entry(oReporte).State = System.Data.Entity.EntityState.Modified;
                _db.SaveChanges();
            };

            string msj = "!Se han editado datos del reporte!";
            ViewBag.msj = msj;
            return View(model);
        }
    }
}

