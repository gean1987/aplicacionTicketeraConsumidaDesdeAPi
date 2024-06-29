using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SERVICE_DESK.Models;
using System.Security.Claims;
using System.Text;

namespace SERVICE_DESK.Controllers
{
    public class TicketAPIController : Controller
    {


        private readonly HttpClient _httpClient;

        public TicketAPIController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:8081/"); // Cambia la URL base según tu configuración
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
                ViewBag.Estados = null;
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

        //grabar
        [HttpPost]
        public async Task<IActionResult> AddTicket(Ticket ticket)
        {
            string condicionEmail = User.FindFirst(ClaimTypes.Email)?.Value;

            try
            {


                // Configurar los datos necesarios antes de enviar la solicitud
                //ticket.Asunto;
                ticket.fechaGeneracion = DateTime.Now;
                ticket.fechaAsignacion = null; // Puedes ajustar según tu lógica
                ticket.fechaCierre = null; // Puedes ajustar según tu lógica
                ticket.correoReceptor = null; // Puedes ajustar según tu lógica
                ticket.correoEmisor = condicionEmail;
                //ticket.equipo;
                ticket.idEstado =1;
                //ticket.tipoConsulta;
                //ticket.cuerpoTicket;
                //ticket.respuestaTicket;

                var json = JsonConvert.SerializeObject(ticket);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("ticket", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var createdTicket = JsonConvert.DeserializeObject<Ticket>(responseData);
                    TempData["mensaje"] = "El ticket se ha actualizado correctamente.";
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


        [HttpPut]
        public async Task<IActionResult> UpdateTicket([FromBody] Ticket ticket)
        {
            try
            {
                var json = JsonConvert.SerializeObject(ticket);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PutAsync("ticket", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "El ticket se ha actualizado correctamente.";
                    return RedirectToAction("MisTickets", "TicketAPI");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    TempData["ErrorMessage"] = "Error al actualizar el ticket: " + errorMessage;
                    return RedirectToAction("MisTickets", "TicketAPI");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error al procesar la solicitud: " + ex.Message;
                return RedirectToAction("MisTickets", "TicketAPI");
            }
        }










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





    }
}
