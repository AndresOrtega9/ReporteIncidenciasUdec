//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReportesUdec.Models
{
    using System;
    using System.Collections.Generic;
    using System.Web;

    public partial class Reporte
    {
        public int Reporte_Id { get; set; }
        public string Ruta_Imagen { get; set; }
        public Nullable<int> Evento_Id { get; set; }
        public string Tipo_Id { get; set; }
        public string Zona_Id { get; set; }
        public string Lugar_Id { get; set; }
        public string Ambiente_Id { get; set; }
        public string Descripcion { get; set; }
        public string Fecha { get; set; }
        public string Estado { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }
    }
}
