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

        //[Autorizacion(idOperacion: 1)]
        public ActionResult Administrador()
        {
            return View();
        }

        //[Autorizacion(idOperacion: 2)]
        public ActionResult PersonalAseo()
        {
            return View();
        }

        public ActionResult PersonalAseoVerReportes()
        {
            using (UdCReportEntities model = new UdCReportEntities())
            {
                return View(model.Reporte.ToList());
            }

        }
        //[Autorizacion(idOperacion: 3)]
        public ActionResult PersonalReparacion()
        {
            return View();
        }

        public ActionResult PersonalReparacionVerReportes()
        {
            using (UdCReportEntities model = new UdCReportEntities())
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
            using (UdCReportEntities model = new UdCReportEntities())
            {
                return View(model.Usuario.ToList());
            }

            //List<ListaUsuariosAdmin> lst;
            //using (UdCReportEntities db = new UdCReportEntities())
            //{
            //    lst = (from d in db.Usuario
            //           select new ListaUsuariosAdmin
            //           {
            //               Usuario_Id = d.Usuario_Id,
            //               Fecha = d.Fecha,
            //               Rol_Id = d.Rol_Id,
            //               Nombre = d.Nombre,
            //               Correo = d.Correo
            //           }).ToList();
            //}
            //return View(lst);
        }

        public ActionResult AdministradorAgregarPersonal()
        {

            return View();

        }

        public ActionResult AdministradorVerReportes()
        {
            using (UdCReportEntities model = new UdCReportEntities())
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
                    using (var _db = new UdCReportEntities())
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

        //public ActionResult EditarReportesAdmin(Reporte model, int Reporte_Id)//a
        //{

        //    using (UdCReportEntities db = new UdCReportEntities())
        //    {
        //        var oReportes = db.Reporte.Find(Reporte_Id);
        //        model.Ruta_Imagen = oReportes.Ruta_Imagen;
        //        model.Evento_Id = oReportes.Evento_Id;
        //        model.Tipo_Id = oReportes.Tipo_Id;
        //        model.Zona_Id = oReportes.Zona_Id;
        //        model.Lugar_Id = oReportes.Lugar_Id;
        //        model.Ambiente_Id = oReportes.Ambiente_Id;
        //        model.Descripcion = oReportes.Descripcion;
        //        model.Fecha = oReportes.Fecha;
        //        model.Estado = oReportes.Estado;
        //        model.Reporte_Id = oReportes.Reporte_Id;

        //    }
        //    return View(model);
        //}

        //[HttpPost]
        //public void EditarReportesAdmin(EditarAdm model, FormCollection Form)
        //{
        //    using (var _db = new UdCReportEntities())
        //    {
        //        var DateAndTime = DateTime.Now;
        //        var fecha = DateAndTime.Date.ToString("yyyy-MM-dd"); var oUsuarios = _db.Usuario.Find(model.reporte_Id);

        //        var oReportes = _db.Reporte.Find(model.reporte_Id);
        //        oReportes.Ruta_Imagen = model.ruta_Imagen;
        //        oReportes.Evento_Id = model.evento_Id;
        //        oReportes.Tipo_Id = model.tipo_Id;
        //        oReportes.Zona_Id = model.zona_Id;
        //        oReportes.Lugar_Id = model.lugar_Id;
        //        oReportes.Ambiente_Id = model.ambiente_Id;
        //        oReportes.Descripcion = model.descripcion;
        //        oReportes.Fecha = model.fecha;
        //        oReportes.Estado = model.estado;


        //        _db.Entry(oReportes).State = System.Data.Entity.EntityState.Modified;
        //        _db.SaveChanges();
        //    }


        //}

        public ActionResult EditarUsuariosAdmin(Usuario model, int id, FormCollection Form)//a
        {

            using (UdCReportEntities db = new UdCReportEntities())
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
        public void EditarUsuariosAdmin(GuardarUsuariosAdm model, FormCollection Form)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (var _db = new UdCReportEntities())
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


                }

            }
            catch (Exception)
            {

                throw;
            }   
        }
    }
}

