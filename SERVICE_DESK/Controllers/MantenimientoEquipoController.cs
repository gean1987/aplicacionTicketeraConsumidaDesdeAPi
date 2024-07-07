using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SERVICE_DESK.Models;
using System.Security.Claims;
using System.Text;

namespace SERVICE_DESK.Controllers
{
    public class MantenimientoEquipoController : Controller
    {

        private readonly HttpClient _httpClient;

        public MantenimientoEquipoController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:8081/"); // Cambia la URL base según tu configuración
        }

        public async Task<IActionResult> Listadoequipos()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("equipo");
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<equipo>>(responseData);
                ViewBag.equipos = data;
            }
            else
            {
                ViewBag.equipos = new List<equipo>();
            }

            return View();
        }



        //metodo para crear
        [HttpPost]
        public async Task<IActionResult> Agregarequipo(equipo datos)
        {

            try
            {
                var json = JsonConvert.SerializeObject(datos);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("equipo", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var createdTicket = JsonConvert.DeserializeObject<Ticket>(responseData);
                    TempData["mensaje"] = "El equipo se ha actualizado correctamente.";
                    return RedirectToAction("Listadoequipos", "Mantenimientoequipo");
                }
                else
                {
                    // Manejo de error si la solicitud no fue exitosa
                    TempData["mensaje"] = "Error al actualizar el equipo: " + response.ReasonPhrase;
                    return RedirectToAction("Listadoequipos", "Mantenimientoequipo");
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                TempData["mensaje"] = "Error al procesar la solicitud: " + ex.Message;
                return RedirectToAction("Listadoequipos", "Mantenimientoequipo");
            }
        }
        //metodo para insertar
        [HttpPost]
        public async Task<IActionResult> Actualizarequipo(equipo datos)
        {
            try
            {

                var json = JsonConvert.SerializeObject(datos);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Realiza la solicitud PUT al endpoint correcto
                var response = await _httpClient.PutAsync("equipo", content);

                string redirectUrl = $"https://localhost:7162/TicketAPI/AbrirTicket?id={datos.idEquipo}";

                if (response.IsSuccessStatusCode)
                {
                    TempData["mensaje"] = "El ticket se ha actualizado correctamente.";
                    TempData["mensajeTipo"] = "success";
                    return RedirectToAction("Listadoequipos", "Mantenimientoequipo");
                }
                else
                {
                    TempData["mensaje"] = "Error al actualizar el ticket: " + response.ReasonPhrase;
                    TempData["mensajeTipo"] = "error";
                    return RedirectToAction("Listadoequipos", "Mantenimientoequipo");
                }
            }
            catch (Exception ex)
            {
                TempData["mensaje"] = "Error al procesar la solicitud: " + ex.Message;
                TempData["mensajeTipo"] = "error";
                return RedirectToAction("Listadoequipos", "Mantenimientoequipo");
            }
        }
        //metodo para eliminar
        [HttpPost]
        public async Task<IActionResult> Eliminarequipo(int id)
        {
            try
            {
                // Realiza la solicitud DELETE al endpoint correcto
                var response = await _httpClient.DeleteAsync($"equipo/{id}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["mensaje"] = "El equipo se ha eliminado correctamente.";
                    TempData["mensajeTipo"] = "success";
                }
                else
                {
                    TempData["mensaje"] = "Error al eliminar el equipo: " + response.ReasonPhrase;
                    TempData["mensajeTipo"] = "error";
                }
            }
            catch (Exception ex)
            {
                TempData["mensaje"] = "Error al procesar la solicitud: " + ex.Message;
                TempData["mensajeTipo"] = "error";
            }

            // Redirige a la acción de listado de equipos
            return RedirectToAction("Listadoequipos", "Mantenimientoequipo");
        }




        public async Task<IActionResult> actualizarequipo(int id)
        {
            // Llamar al endpoint con el ID del ticket

            var rol = "Receptor";

            HttpResponseMessage response = await _httpClient.GetAsync($"equipo/{id}");
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var equipos = JsonConvert.DeserializeObject<equipo>(responseData);
                ViewBag.equipos = equipos;
            }
            else
            {
                ViewBag.equipos = null;
            }
            return View();
        }


        public async Task<IActionResult> Crearequipo()
        {

            return View();
        }


        public async Task<IActionResult> EliminarDatosequipo(int id)
        {
            // Llamar al endpoint con el ID del ticket

            var rol = "Receptor";

            HttpResponseMessage response = await _httpClient.GetAsync($"equipo/{id}");
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var equipos = JsonConvert.DeserializeObject<equipo>(responseData);
                ViewBag.equipos = equipos;
            }
            else
            {
                ViewBag.equipos = null;
            }
            return View();
        }




    }
}
