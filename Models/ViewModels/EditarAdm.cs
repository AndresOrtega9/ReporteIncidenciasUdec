using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportesUdec.Models.ViewModels
{
    public class EditarAdm
    {
       
        public int reporte_Id { get; set; }
       
        public string ruta_Imagen { get; set; }
       
        public Nullable<int> evento_Id { get; set; }
        
        public string tipo_Id { get; set; }
        
        public string zona_Id { get; set; }
        
        public string lugar_Id { get; set; }
       
        public string ambiente_Id { get; set; }
       
        public string descripcion { get; set; }
        
        public string fecha { get; set; }
       
        public string estado { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}