using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReportesUdec.Models
{
    public class EventoTipo
    {
        public EventoTipo()
        {
            this.Eventos = new List<SelectListItem>();
            this.Tipos = new List<SelectListItem>();
            
        }

        public List<SelectListItem> Eventos { get; set; }
        public List<SelectListItem> Tipos { get; set; }
      

        public int EventoId { get; set; }
        public int TipoId { get; set; }
       
    }
}