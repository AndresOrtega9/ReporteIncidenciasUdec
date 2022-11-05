using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReportesUdec.Models.ViewModels
{
    public class RecuperarModel
    {      
        [EmailAddress]
        [Required]
        public string Correo { get; set; }
    }
}