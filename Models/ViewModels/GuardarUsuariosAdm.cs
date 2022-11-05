using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReportesUdec.Models.ViewModels
{
    public class GuardarUsuariosAdm
    {
        public int Usuario_Id { get; set; }
        
        [Display(Name ="Nombre de usuario")]
        //[Required(ErrorMessage = "{0} es requerido")]
        public string Nombre { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Correo electrónico")]
        public string Correo { get; set; }
        [Required]
        [Display(Name = "Contraseña")]
        public string Contraseña { get; set; }
        public string Fecha { get; set; }
        [Display(Name = "Ocupación")]
        public Nullable<int> Rol_Id { get; set; }
    }
}