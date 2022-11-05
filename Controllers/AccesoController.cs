using ReportesUdec.DbOp;
using ReportesUdec.Models;
using Rotativa;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace ReportesUdec.Controllers
{
    public class AccesoController : Controller
    {
       
        string urlDominio = "https://localhost:44353/";
        //string urlDominio = "https://reportesudec20221101014254.azurewebsites.net/";

        [HttpGet]
        public ActionResult InicioRecuperacion()
        {
            Models.ViewModels.RecuperarModel model = new Models.ViewModels.RecuperarModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult InicioRecuperacion(Models.ViewModels.RecuperarModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                string token = GetSha256(Guid.NewGuid().ToString());
                using (Models.UdCReportEntities _db = new Models.UdCReportEntities())
                {
                    var oUser = _db.Usuario.Where(d => d.Correo == model.Correo).FirstOrDefault();
                    if (oUser != null)
                    {
                        oUser.Token = token;
                        _db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        _db.SaveChanges();

                        EnviarCorreo(oUser.Correo, token);
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string GetSha256(string str)
        {
            SHA1 sha1 = SHA1Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha1.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        private void EnviarCorreo(string CorreoDestino, string token)
        {
            string CorreoDeOrigen = "equispruebas02@gmail.com";
            string Contraseña = "ydoheqygfwdzxvnp";
            string url = urlDominio + "/Acceso/Recuperacion/?token=" + token;
            MailMessage oCorreo = new MailMessage(CorreoDeOrigen, CorreoDestino, "Recuperación de contraseña",
                "<p>De UdC Report</p><br>" +
                "<a href='" + url + "'>Click para recuperar</a>");
            oCorreo.IsBodyHtml = true;
            SmtpClient oSmtpClient = new SmtpClient("smtp.gmail.com");
            oSmtpClient.EnableSsl = true;
            oSmtpClient.UseDefaultCredentials = false;
            oSmtpClient.Port = 587;// Puerto que utiliza Gmail para sus servicios
                                   //Especificamos las credenciales con las que enviaremos el mail
            oSmtpClient.Credentials = new System.Net.NetworkCredential(CorreoDeOrigen, Contraseña);
            oSmtpClient.Send(oCorreo);
            oSmtpClient.Dispose();
        }

        [HttpGet]
        public ActionResult Recuperacion(string token)
        {
            Models.ViewModels.NuevaContraseña model = new Models.ViewModels.NuevaContraseña();
            model.token = token;
            using (Models.UdCReportEntities _db = new Models.UdCReportEntities())
            {
                if (model.token == null || model.token.Trim().Equals(""))
                {
                    return View("Login");
                }
                var oUser = _db.Usuario.Where(d => d.Token == model.token).FirstOrDefault();
                if (oUser == null)
                {
                    ViewBag.Error = "Su token ya venció";
                    return View("Login");
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Recuperacion(Models.ViewModels.NuevaContraseña model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                using (Models.UdCReportEntities _db = new Models.UdCReportEntities())
                {
                    var oUser = _db.Usuario.Where(d => d.Token == model.token).First();

                    if (oUser != null)
                    {
                        oUser.Contraseña = model.Contraseña1;
                        oUser.Token = null;
                        _db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                        _db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            ViewBag.Message = "Su contraseña se ha establecido";
            return View("Login");
        }

        public ActionResult Login()
        {
           
            return View();
           
        }

        [HttpPost]
        public ActionResult Login(string user, string pass)
        {
            try
            {
                using (Models.UdCReportEntities _db = new Models.UdCReportEntities())
                {
                    var oUser = (from d in _db.Usuario
                                 where d.Correo == user.Trim() && d.Contraseña == pass.Trim()
                                 select d).FirstOrDefault();
                    if (oUser == null)
                    {
                        ViewBag.Error = "Usuario o contraseña no existen";
                        return View();
                    }
                    Session["user"] = oUser;
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }


        MngReporte mngRep = new MngReporte();


        public ActionResult CrearReporte()
        {
            return View();
        }


        [HttpPost]
        public void GuardarReporte(FormCollection Form, Reporte rpt)
        {
            //string filename = Path.GetFileNameWithoutExtension(rpt.ImageFile.FileName);
            //string ext = Path.GetExtension(rpt.ImageFile.FileName);
            //filename = filename + DateTime.Now.ToString("yymmssfff") + ext;
            //rpt.Ruta_Imagen = "../Imagenes/" + filename;
            //filename = Path.Combine(Server.MapPath("../Imagenes/"), filename);
            //rpt.ImageFile.SaveAs(filename);

            using (var _db = new UdCReportEntities())
            {

                var DateAndTime = DateTime.Now;
                var fecha = DateAndTime.Date.ToString("yyyy-MM-dd");

                Reporte rm = new Reporte()
                {
                    Estado = "Activo",
                    Fecha = fecha,
                    //Ruta_Imagen = rpt.Ruta_Imagen,
                    Evento_Id = int.Parse(Form["txtEvento"]),
                    Tipo_Id = Form["txtTipo"],
                    Zona_Id = Form["txtZona"],
                    Descripcion = Form["txtDescripcion"]

                };
                mngRep.GuardarReporte(rm);
                Response.Redirect("CrearReporte");

            }

        }

        DatosDDl datos = new DatosDDl();


        public string LLenarEvento()
        {
            return datos.ListaEvento();
        }

        //public string LlenarTipo(int EventoId)
        //{
        //    return datos.ListaTipo(EventoId);
        //}

        //public string LLenarZona()
        //{
        //    return datos.ListaZona();
        //}

        //public string LlenarLugar(int ZonaId)
        //{
        //    return datos.ListaLugar(ZonaId);
        //}

        //public string LlenarAmbiente(int LugarId)
        //{
        //    return datos.ListaAmbiente(LugarId);
        //}

        // GET: CargarReporte
        public ActionResult CargarReporte()
        {
            //List<SelectListItem> ListaEvento = new List<SelectListItem>();

            //using (Models.UdCReportEntities db = new Models.UdCReportEntities())
            //{
            //    ListaEvento = (from d in db.Evento
            //             select new SelectListItem
            //             {
            //                 Value = d.Evento_Id.ToString(),
            //                 Text = d.Evento_Nombre
            //             }).ToList();
            //}

            //List<SelectListItem> ListaZona = new List<SelectListItem>();
            //using (Models.UdCReportEntities db = new Models.UdCReportEntities())
            //{
            //    ListaZona = (from d in db.Zona
            //             select new SelectListItem
            //             {
            //                 Value = d.Zona_Id.ToString(),
            //                 Text = d.Zona_Nombre
            //             }).ToList();
            //}

            //ViewBag.ListaEvento = ListaEvento;
            //ViewBag.ListaZona = ListaZona;
            return View();
        }


        public ActionResult ind()
        {
            List<SelectListItem> ListaEvento = new List<SelectListItem>();

            using (Models.UdCReportEntities db = new Models.UdCReportEntities())
            {
                ListaEvento = (from d in db.Evento
                               select new SelectListItem
                               {
                                   Value = d.Evento_Id.ToString(),
                                   Text = d.Evento_Nombre
                               }).ToList();
            }

            List<SelectListItem> ListaZona = new List<SelectListItem>();
            using (Models.UdCReportEntities db = new Models.UdCReportEntities())
            {
                ListaZona = (from d in db.Zona
                             select new SelectListItem
                             {
                                 Value = d.Zona_Id.ToString(),
                                 Text = d.Zona_Nombre
                             }).ToList();
            }

            ViewBag.ListaEvento = ListaEvento;
            ViewBag.ListaZona = ListaZona;
            return View();
        }



        [HttpGet]
        public JsonResult Tipo(int IdEvento)
        {
            List<ElementoJson> lista = new List<ElementoJson>();
            using (Models.UdCReportEntities db = new UdCReportEntities())
            {
                lista = (from d in db.Tipo
                         where d.idEvento == IdEvento
                         select new ElementoJson
                         {
                             Value = d.Tipo_Id,
                             Text = d.Tipo_Nombre
                         }).ToList();
            }
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

        public class ElementoJson
        {
            public int Value { get; set; }
            public string Text { get; set; }

        }

        //SIN DDL
        public ActionResult NuevoReporte()
        {
            return View();
        }

        [HttpPost]
        public void CargarNuevoReporte(Reporte model, FormCollection Form)
        {
          
                    using (var _db = new UdCReportEntities())
                    {
                        var DateAndTime = DateTime.Now;
                        var fecha = DateAndTime.Date.ToString("yyyy-MM-dd");

                        Reporte rm = new Reporte()
                        {
                            Ruta_Imagen = Form["ImageFile"],
                            Evento_Id = model.Evento_Id,
                            Tipo_Id = model.Tipo_Id,
                            Zona_Id = model.Zona_Id,
                            Descripcion = model.Descripcion,
                            Fecha = fecha,
                            Estado = "Activo"                          
                        };

                        mngRep.CargarNuevoReporte(rm);
                        Response.Write("Reporte creado con éxito");
                
    
                        
            }
        }
    }
}