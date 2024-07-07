using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SERVICE_DESK.Models;
using System.Security.Claims;
using System.Text;

using HtmlAgilityPack;
using SERVICE_DESK.Gestiones;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using SERVICE_DESK.Enums;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Diagnostics;
//using ClosedXML.Excel;
using System.IO;
using System.Data;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Routing;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Http.Extensions;

namespace SERVICE_DESK.Controllers
{
    public class TicketAPIController : Controller
    {


        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration Configuration;
        private readonly IUrlHelperFactory _urlHelperFactory;

        public TicketAPIController(IHttpClientFactory httpClientFactory, IConfiguration configuration, IUrlHelperFactory urlHelperFactory = null)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:8081/"); // Cambia la URL base según tu configuración
            Configuration = configuration;
            _urlHelperFactory = urlHelperFactory;


        }

        //VistaAdministrador
        public async Task<IActionResult> ListadoGeneral()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("ticket");
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var tickets = JsonConvert.DeserializeObject<List<Ticket>>(responseData);
                ViewBag.Tickets = tickets;
            }
            else
            {
                ViewBag.Tickets = new List<Ticket>();
            }
            // Llamar al endpoint para obtener los estados
            HttpResponseMessage estadosResponse = await _httpClient.GetAsync("estado");
            if (estadosResponse.IsSuccessStatusCode)
            {
                string estadosData = await estadosResponse.Content.ReadAsStringAsync();
                var estados = JsonConvert.DeserializeObject<List<Estado>>(estadosData);
                ViewBag.Estados = estados;
            }
            else
            {
                ViewBag.Estados = new List<Estado>();
            }

            // Llamar al endpoint para obtener los equipos
            HttpResponseMessage equipoResponse = await _httpClient.GetAsync("equipo");
            if (equipoResponse.IsSuccessStatusCode)
            {
                string equipoData = await equipoResponse.Content.ReadAsStringAsync();
                var equipos = JsonConvert.DeserializeObject<List<equipo>>(equipoData);
                ViewBag.Equipos = equipos;
            }
            else
            {
                ViewBag.Equipos = new List<equipo>();
            }

            // Llamar al endpoint para obtener los tipos de consulta
            HttpResponseMessage tipoConsultaResponse = await _httpClient.GetAsync("tipoConsulta");
            if (tipoConsultaResponse.IsSuccessStatusCode)
            {
                string tipoConsultaData = await tipoConsultaResponse.Content.ReadAsStringAsync();
                var tipoConsulta = JsonConvert.DeserializeObject<List<tipoConsulta>>(tipoConsultaData);
                ViewBag.TipoConsulta = tipoConsulta;
            }
            else
            {
                ViewBag.TipoConsulta = new List<tipoConsulta>();
            }

            return View();

        }
        //VistaResponsable
        public async Task<IActionResult> MisTicketsAsignados()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("ticket");
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var tickets = JsonConvert.DeserializeObject<List<Ticket>>(responseData);
                ViewBag.Tickets = tickets;
            }
            else
            {
                ViewBag.Tickets = new List<Ticket>();
            }
            // Llamar al endpoint para obtener los estados
            HttpResponseMessage estadosResponse = await _httpClient.GetAsync("estado");
            if (estadosResponse.IsSuccessStatusCode)
            {
                string estadosData = await estadosResponse.Content.ReadAsStringAsync();
                var estados = JsonConvert.DeserializeObject<List<Estado>>(estadosData);
                ViewBag.Estados = estados;
            }
            else
            {
                ViewBag.Estados = new List<Estado>();
            }

            // Llamar al endpoint para obtener los equipos
            HttpResponseMessage equipoResponse = await _httpClient.GetAsync("equipo");
            if (equipoResponse.IsSuccessStatusCode)
            {
                string equipoData = await equipoResponse.Content.ReadAsStringAsync();
                var equipos = JsonConvert.DeserializeObject<List<equipo>>(equipoData);
                ViewBag.Equipos = equipos;
            }
            else
            {
                ViewBag.Equipos = new List<equipo>();
            }

            // Llamar al endpoint para obtener los tipos de consulta
            HttpResponseMessage tipoConsultaResponse = await _httpClient.GetAsync("tipoConsulta");
            if (tipoConsultaResponse.IsSuccessStatusCode)
            {
                string tipoConsultaData = await tipoConsultaResponse.Content.ReadAsStringAsync();
                var tipoConsulta = JsonConvert.DeserializeObject<List<tipoConsulta>>(tipoConsultaData);
                ViewBag.TipoConsulta = tipoConsulta;
            }
            else
            {
                ViewBag.TipoConsulta = new List<tipoConsulta>();
            }

            return View();
        }


        //vistaEmisor
        public async Task<IActionResult> MisTickets()
        {
            // Llamar al endpoint para obtener los tickets
            HttpResponseMessage response = await _httpClient.GetAsync("ticket");
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var tickets = JsonConvert.DeserializeObject<List<Ticket>>(responseData);
                ViewBag.Tickets = tickets;
            }
            else
            {
                ViewBag.Tickets = new List<Ticket>();
            }

            // Llamar al endpoint para obtener los estados
            HttpResponseMessage estadosResponse = await _httpClient.GetAsync("estado");
            if (estadosResponse.IsSuccessStatusCode)
            {
                string estadosData = await estadosResponse.Content.ReadAsStringAsync();
                var estados = JsonConvert.DeserializeObject<List<Estado>>(estadosData);
                ViewBag.Estados = estados;
            }
            else
            {
                ViewBag.Estados = new List<Estado>();
            }

            // Llamar al endpoint para obtener los equipos
            HttpResponseMessage equipoResponse = await _httpClient.GetAsync("equipo");
            if (equipoResponse.IsSuccessStatusCode)
            {
                string equipoData = await equipoResponse.Content.ReadAsStringAsync();
                var equipos = JsonConvert.DeserializeObject<List<equipo>>(equipoData);
                ViewBag.Equipos = equipos;
            }
            else
            {
                ViewBag.Equipos = new List<equipo>();
            }

            // Llamar al endpoint para obtener los tipos de consulta
            HttpResponseMessage tipoConsultaResponse = await _httpClient.GetAsync("tipoConsulta");
            if (tipoConsultaResponse.IsSuccessStatusCode)
            {
                string tipoConsultaData = await tipoConsultaResponse.Content.ReadAsStringAsync();
                var tipoConsulta = JsonConvert.DeserializeObject<List<tipoConsulta>>(tipoConsultaData);
                ViewBag.TipoConsulta = tipoConsulta;
            }
            else
            {
                ViewBag.TipoConsulta = new List<tipoConsulta>();
            }

            return View();
        }

        //abrir ticket por id
        public async Task<IActionResult> AbrirTicket(int id)
        {
            // Llamar al endpoint con el ID del ticket

            var rol = "Receptor";

            HttpResponseMessage response = await _httpClient.GetAsync($"ticket/{id}");
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var ticket = JsonConvert.DeserializeObject<Ticket>(responseData);
                ViewBag.Ticket = ticket;
            }
            else
            {
                ViewBag.Ticket = null;
            }

            // Llamar al endpoint para obtener los estados
            HttpResponseMessage estadosResponse = await _httpClient.GetAsync("estado");
            if (estadosResponse.IsSuccessStatusCode)
            {
                string estadosData = await estadosResponse.Content.ReadAsStringAsync();
                var estados = JsonConvert.DeserializeObject<List<Estado>>(estadosData);
                ViewBag.Estados = estados;
            }
            else
            {
                ViewBag.Estados = null;
            }

            HttpResponseMessage equipoResponse = await _httpClient.GetAsync("equipo");
            if (equipoResponse.IsSuccessStatusCode)
            {
                string equipoData = await equipoResponse.Content.ReadAsStringAsync();
                var equipos = JsonConvert.DeserializeObject<List<equipo>>(equipoData);
                ViewBag.Equipos = equipos;
            }
            else
            {
                ViewBag.Equipos = new List<equipo>();
            }

            // Llamar al endpoint para obtener los tipos de consulta
            HttpResponseMessage tipoConsultaResponse = await _httpClient.GetAsync("tipoConsulta");
            if (tipoConsultaResponse.IsSuccessStatusCode)
            {
                string tipoConsultaData = await tipoConsultaResponse.Content.ReadAsStringAsync();
                var tipoConsulta = JsonConvert.DeserializeObject<List<tipoConsulta>>(tipoConsultaData);
                ViewBag.TipoConsulta = tipoConsulta;
            }
            else
            {
                ViewBag.TipoConsulta = new List<tipoConsulta>();
            }





            HttpResponseMessage usuarioResponse = await _httpClient.GetAsync($"responsable/{rol}"); ;
            if (usuarioResponse.IsSuccessStatusCode)
            {
                string usuarioData = await usuarioResponse.Content.ReadAsStringAsync();
                var usuario = JsonConvert.DeserializeObject<List<Usuario>>(usuarioData);
                ViewBag.responsables = usuario;
            }
            else
            {
                ViewBag.responsables = null;
            }







            return View();
        }

        //metodo para crear
        [HttpPost]
        public async Task<IActionResult> AddTicket(Ticket ticket)
        {
            string condicionEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            try
            {
                
                ticket.fechaGeneracion = DateTime.Now;
                ticket.fechaAsignacion = null; // Puedes ajustar según tu lógica
                ticket.fechaCierre = null; // Puedes ajustar según tu lógica
                ticket.correoReceptor = null; // Puedes ajustar según tu lógica
                ticket.correoEmisor = condicionEmail;
                //ticket.equipo;
                ticket.idEstado =1;
               

                var json = JsonConvert.SerializeObject(ticket);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("ticket", content);

                var urlHelper = _urlHelperFactory.GetUrlHelper(ControllerContext);
                string enlace = $"https://localhost:7162/{urlHelper.Action("ListadoGeneral", "TicketAPI")}";

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var createdTicket = JsonConvert.DeserializeObject<Ticket>(responseData);
                    TempData["mensaje"] = "El ticket se ha actualizado correctamente.";
                    EnviarCorreoElectronico("Hay un nuevo ticket de asunto:", ticket.asunto, ticket.cuerpoTicket, "SERVICEDESKLogistica@outlook.com", enlace);

                    return RedirectToAction("MisTickets", "TicketAPI");



                }
                else
                {
                    // Manejo de error si la solicitud no fue exitosa
                    TempData["mensaje"] = "Error al actualizar el ticket: " + response.ReasonPhrase;
                    return RedirectToAction("MisTickets", "TicketAPI");
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                TempData["mensaje"] = "Error al procesar la solicitud: " + ex.Message;
                return RedirectToAction("MisTickets", "TicketAPI");
            }
        }
        //metodo para insertar
        [HttpPost]
        public async Task<IActionResult> ActualizarTicket(Ticket ticket)
        {
            try
            {
                // Ajusta la fecha de cierre según tu lógica
                ticket.fechaCierre = DateTime.Now;

                // Serializa el objeto Ticket a JSON
                var json = JsonConvert.SerializeObject(ticket);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Realiza la solicitud PUT al endpoint correcto
                var response = await _httpClient.PutAsync("ticket", content);

                string redirectUrl = $"https://localhost:7162/TicketAPI/AbrirTicket?id={ticket.idTicket}";

                var urlHelper = _urlHelperFactory.GetUrlHelper(ControllerContext);
                string enlace = $"https://localhost:7162/{urlHelper.Action("AbrirTicket", "TicketAPI")}?id={ticket.idTicket}";

                if (response.IsSuccessStatusCode)
                {
                    TempData["mensaje"] = "El ticket se ha actualizado correctamente.";
                    TempData["mensajeTipo"] = "success";

                    if(ticket.idEstado ==2 &&( ticket.correoReceptor != null ))
                    {
                        EnviarCorreoElectronico("Hay un nuevo ticket de asunto:", ticket.asunto, ticket.cuerpoTicket, ticket.correoReceptor, enlace);

                    }
                    if (ticket.idEstado == 3 && (ticket.correoEmisor != null ))
                    {
                        EnviarCorreoElectronico("Su ticket ah sido atendido:", ticket.asunto, ticket.cuerpoTicket, ticket.correoReceptor, enlace);

                    }


                    return Redirect(redirectUrl);
                }
                else
                {
                    TempData["mensaje"] = "Error al actualizar el ticket: " + response.ReasonPhrase;
                    TempData["mensajeTipo"] = "error";
                    return Redirect(redirectUrl);
                }
            }
            catch (Exception ex)
            {
                TempData["mensaje"] = "Error al procesar la solicitud: " + ex.Message;
                TempData["mensajeTipo"] = "error";
                return RedirectToAction(nameof(AbrirTicket), new { id = ticket.idTicket });
            }
        }


       






        //filtrado de ticket
        [HttpGet]
        public async Task<IActionResult> SearchTickets(int idEstado, int tipoFecha, DateTime fechaInicio, DateTime fechaFin)
        {
            // Construir la URL de búsqueda con los parámetros
            string url = $"searchTickets?idEstado={idEstado}&tipoFecha={tipoFecha}&fechaInicio={fechaInicio.ToString("yyyy-MM-dd")}&fechaFin={fechaFin.ToString("yyyy-MM-dd")}";

            // Llamar al endpoint para buscar los tickets
            HttpResponseMessage response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var tickets = JsonConvert.DeserializeObject<List<Ticket>>(responseData);
                ViewBag.Tickets = tickets;
            }
            else
            {
                ViewBag.Tickets = new List<Ticket>();
            }

            // Llamar al endpoint para obtener los estados
            HttpResponseMessage estadosResponse = await _httpClient.GetAsync("estado");
            if (estadosResponse.IsSuccessStatusCode)
            {
                string estadosData = await estadosResponse.Content.ReadAsStringAsync();
                var estados = JsonConvert.DeserializeObject<List<Estado>>(estadosData);
                ViewBag.Estados = estados;
            }
            else
            {
                ViewBag.Estados = new List<Estado>();
            }

            // Llamar al endpoint para obtener los equipos
            HttpResponseMessage equipoResponse = await _httpClient.GetAsync("equipo");
            if (equipoResponse.IsSuccessStatusCode)
            {
                string equipoData = await equipoResponse.Content.ReadAsStringAsync();
                var equipos = JsonConvert.DeserializeObject<List<equipo>>(equipoData);
                ViewBag.Equipos = equipos;
            }
            else
            {
                ViewBag.Equipos = new List<equipo>();
            }

            // Llamar al endpoint para obtener los tipos de consulta
            HttpResponseMessage tipoConsultaResponse = await _httpClient.GetAsync("tipoConsulta");
            if (tipoConsultaResponse.IsSuccessStatusCode)
            {
                string tipoConsultaData = await tipoConsultaResponse.Content.ReadAsStringAsync();
                var tipoConsulta = JsonConvert.DeserializeObject<List<tipoConsulta>>(tipoConsultaData);
                ViewBag.TipoConsulta = tipoConsulta;
            }
            else
            {
                ViewBag.TipoConsulta = new List<tipoConsulta>();
            }

            return View("MisTickets");
        }





        private void EnviarCorreoElectronico(string ticketEnviado, string asunto, string cuerpo, string destinatario,  string enlace)
        {
            string asuntoSeteado = ticketEnviado + ' ' + asunto;
            {
                try
                {
                    using (SmtpClient smtpClient = new SmtpClient
                    {
                        Host = Configuration["SmtpSettings:Host"],
                        Port = int.Parse(Configuration["SmtpSettings:Port"]),
                        Credentials = new NetworkCredential(Configuration["SmtpSettings:UserName"], Configuration["SmtpSettings:Password"]),
                        EnableSsl = bool.Parse(Configuration["SmtpSettings:EnableSsl"])
                    })
                    {
                        MailMessage mail = new MailMessage();
                        mail.From = new MailAddress("SERVICEDESKLogistica@outlook.com");

                        // Separar múltiples destinatarios utilizando comas
                        string[] destinatarios = destinatario.Split(',');

                        foreach (string destinatarioIndividual in destinatarios)
                        {
                            mail.To.Add(destinatarioIndividual.Trim());
                        }
                        mail.Subject = asuntoSeteado;
                        string body = $@"
            <html>
            <body style='font-family: Arial, sans-serif;'>
                <p>De categoría <strong></strong>.</p>
                <p>Para abrir el ticket, por favor siga este enlace:</p>
                <div style='text-align: center; margin: 20px 0;'>
                    <a href='{enlace}' style='display: inline-block; padding: 10px 20px; font-size: 16px; color: #fff; background-color: #3696fb; text-align: center; text-decoration: none; border-radius: 5px;'>Abrir Ticket</a>
                </div>
                <p>¡Favor de NO responder este correo! </p>
                <p>Atentamente, </p>
                <p>Service Desk Logística </p>

            </body>
            </html>";

                        mail.Body = body;
                        mail.IsBodyHtml = true;

                      

                        smtpClient.Send(mail);
                    }
                }
                catch (Exception ex)
                {
                    // Manejar cualquier error al enviar el correo
                    throw new Exception("Error al enviar el correo electrónico: " + ex.Message, ex);
                }
            }

        }










    }
}
