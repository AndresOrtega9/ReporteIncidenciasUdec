using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ReportesUdec.Models;
using System.Web.Mvc;
using System.IO;
using ReportesUdec.Filtros;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;
using System.Web.UI;

namespace ReportesUdec.DbOp
{
    public class MngReporte 
    {
        [HttpPost]
        public void GuardarReporte(Reporte repMod)
        {
           
            using (var _db = new UdCReportEntities())
            {             
                Reporte r = new Reporte()
                {
                    //Ruta_Imagen=repMod.Ruta_Imagen,
                    Evento_Id = repMod.Evento_Id,
                    Tipo_Id = repMod.Tipo_Id,
                    Zona_Id = repMod.Zona_Id,
                    Descripcion = repMod.Descripcion,
                    Fecha=repMod.Fecha,
                    Estado=repMod.Estado
                   
                };
                _db.Reporte.Add(r);
                _db.SaveChanges();
            }                   
        }

        [HttpPost]
        public void Nuevo(Usuario user)
        {

            using (var _db = new UdCReportEntities())
            {
                Usuario u = new Usuario()
                {
                    Nombre = user.Nombre,
                    Correo = user.Correo,
                    Contraseña = user.Contraseña,
                    Fecha = user.Fecha,
                    Rol_Id=user.Rol_Id
                };
                _db.Usuario.Add(u);
                _db.SaveChanges();
            }         
        }

        //SIN DDL
        public void CargarNuevoReporte(Reporte rep)
        {

            using (var _db = new UdCReportEntities())
            {
                Reporte r = new Reporte()
                {
                    Ruta_Imagen= rep.Ruta_Imagen,
                    Evento_Id=rep.Evento_Id,
                    Tipo_Id=rep.Tipo_Id,
                    Zona_Id=rep.Zona_Id,
                    Descripcion=rep.Descripcion,
                    Fecha=rep.Fecha,
                    Estado=rep.Estado,
                    
                };
                _db.Reporte.Add(r);
                _db.SaveChanges();
            }
        }

        public string ListaDeDaños()
        {

            using (var _db = new UdCReportEntities())
            {
                var Data = _db.Reporte.ToList();
                var Json = JsonConvert.SerializeObject(Data);
                return Json;
            }
        }

        public string ListaDeReportesAdm()
        {

            using (var _db = new UdCReportEntities())
            {
                var Data = _db.Reporte.ToList();
                var Json = JsonConvert.SerializeObject(Data);
                return Json;
            }
        }

        public string ListaDeAseo()
        {

            using (var _db = new UdCReportEntities())
            {
                var Data = _db.Reporte.ToList();
                var Json = JsonConvert.SerializeObject(Data);
                return Json;
            }
        }

        public void EliminarRep(int Id)
        {
            using (var db = new UdCReportEntities())
            {
                var Data = db.Usuario.Find(Id);
                if (Data!=null)
                {
                    db.Usuario.Remove(Data);
                    db.SaveChanges();
                }
            }
        }
    }
}