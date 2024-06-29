using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SERVICE_DESK.Enums
{
    public enum Operacion
    {
        /*Ticket*/
        ListadoTickets = 1,
        MisTickets = 2,
        AsignarTicket = 3,
        ActualizarEstadoTicket = 4,
        ReporteGeneralTickets = 5,
        ReportePersonalTickets = 6,

        /*Usuario*/
        ListadoUsuarios = 7,

        /*Categoría*/
        ListadoCategorias = 8,
        ActualizarPrioridadTicket = 9,
        contadorTickets = 10,

        //modificacion notificaion
        NotificacionTicket = 11,

        MisTicketsAsignados = 12,

    }
}