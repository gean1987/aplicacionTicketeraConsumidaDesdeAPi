using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace SERVICE_DESK.Models
{
    public class Ticket
    {
        public int idTicket { get; set; }
        public DateTime fechaGeneracion { get; set; }
        public DateTime? fechaAsignacion { get; set; }
        public DateTime? fechaCierre { get; set; }
        public int idEstado { get; set; }
        //modificacion
        public string asunto { get; set; }

        ///modificacion conte
        //   
        //relacionado al contenido del ticket
        public string cuerpoTicket { get; set; }
        public string respuestaTicket { get; set; }
        public int equipo { get; set; }
        public int tipoConsulta { get; set; }

        public string correoReceptor { get; set; }
        public string correoEmisor { get; set; }
        public string GetFechaFormateada(DateTime? fecha)
        {
            if (fecha != null)
            {
                return fecha.Value.ToString("dd MMM , yyyy hh:mm tt");
            }
            else
            {
                return "-";
            }
        }

        public string GetFechaFormateadaTickets(DateTime? fecha)
        {
            if (fecha != null)
            {
                return fecha.Value.ToString("dd MMM , yyyy");
            }
            else
            {
                return "-";
            }
        }

        public string GetFechaFormateadaTicket(DateTime? fecha)
        {
            if (fecha != null)
            {
                return fecha.Value.ToString("dd/MM/yy");
            }
            else
            {
                return "-";
            }
        }




    }
}