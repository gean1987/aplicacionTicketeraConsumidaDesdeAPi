using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SERVICE_DESK.Models
{
    public class Estado
    {
        public int IdEstado { get; set; }
        public string Nombre { get; set; }
        public string descripcion { get; set; }
        public bool EsActivo { get; set; }
    }
}