using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReportesUdec.Models.ViewModels
{
    public class NuevaContraseña
    {
        public string token { get; set; }
        [Required]
        public string Contraseña1 { get; set; }
        [Compare("Contraseña1")]
        [Required]
        public string Contraseña2 { get; set; }
    }
}