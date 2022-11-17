using ReportesUdec.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportesUdec.Filtros
{
    [AttributeUsage(AttributeTargets.Method,AllowMultiple =false)]
    public class Autorizacion : AuthorizeAttribute
    {
        private Usuario oUsuario;
        private ReportesUdec_dbEntities _db = new ReportesUdec_dbEntities();
        private int idOperacion;

        public Autorizacion(int idOperacion = 0)
        {
            this.idOperacion = idOperacion;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            String nombreOperacion = "";
            String nombreModulo = "";
            try
            {
                oUsuario = (Usuario)HttpContext.Current.Session["user"];
                var listaOperaciones = from m in _db.Rol_Operacion
                                       where m.Rol_Id==oUsuario.Rol_Id
                                       && m.Operacion_Id==idOperacion
                                       select m;
                if (listaOperaciones.ToList().Count() == 0)
                {
                    var oOperacion = _db.Operaciones.Find(idOperacion);
                    int? idModulo = oOperacion.Modulo_Id;
                    nombreOperacion = getNombreOperacion(idOperacion);
                    nombreModulo = getNombreModulo(idModulo);
                    filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?Operacion=" + nombreOperacion + "&modulo=" + nombreModulo + "&msjeErrorExcepcion=");
                }

            }catch(Exception ex)
            {
                filterContext.Result = new RedirectResult("~/Error/UnauthorizedOperation?Operacion=" + nombreOperacion + "&modulo=" + nombreModulo + "&msjeErrorExcepcion=");
            }
            
        }

        public string getNombreOperacion(int idOperacion)
        {
            var ope = from op in _db.Operaciones
                      where op.Operaciones_Id == idOperacion
                      select op.Operaciones_Nombre;
            String nombreOperacion;
            try
            {
                nombreOperacion = ope.First();

            }catch(Exception ex)
            {
                nombreOperacion = "";

            }
            return nombreOperacion;
        }

        public string getNombreModulo(int? idModulo)
        {
            var modulo = from m in _db.Modulo
                         where m.Modulo_Id == idModulo
                         select m.Modulo_Nombre;
            String nombreModulo;
            try
            {
                nombreModulo = modulo.First();

            }catch(Exception ex)
            {
                nombreModulo = "";

            }
            return nombreModulo;
        }
    }
}