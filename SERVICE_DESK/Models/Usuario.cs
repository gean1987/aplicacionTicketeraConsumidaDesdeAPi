using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SERVICE_DESK.Models
{
    public class Usuario
    {
        public int idUsuario { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correo { get; set; }


        public string rol { get; set; }
       
        public string contraseña { get; set; }







    }
}