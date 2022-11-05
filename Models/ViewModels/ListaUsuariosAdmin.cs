using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReportesUdec.Models.ViewModels
{
    public class ListaUsuariosAdmin
    {
        public int Usuario_Id { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public string Fecha { get; set; }
        public Nullable<int> Rol_Id { get; set; }
    }
}