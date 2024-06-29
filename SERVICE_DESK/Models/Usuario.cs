using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SERVICE_DESK.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }


        public string Rol { get; set; }
       
        public string contrasena { get; set; }







    }
}