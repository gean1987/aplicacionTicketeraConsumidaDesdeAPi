using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SERVICE_DESK.Models;
using System.Security.Claims;
using System.Text;
namespace SERVICE_DESK.Controllers
{
    public class MantenimientoTipoConsultaController : Controller
    {

        private readonly HttpClient _httpClient;

        public MantenimientoTipoConsultaController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:8081/"); // Cambia la URL base según tu configuración
        }

        public async Task<IActionResult> ListadotipoConsulta()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("tipoConsulta");
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<tipoConsulta>>(responseData);
                ViewBag.tipoConsulta = data;
            }
            else
            {
                ViewBag.tipoConsulta = new List<tipoConsulta>();
            }

            return View();
        }



        //metodo para crear
        [HttpPost]
        public async Task<IActionResult> AgregartipoConsulta(tipoConsulta datos)
        {

            try
            {
                var json = JsonConvert.SerializeObject(datos);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("tipoConsulta", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var createdTicket = JsonConvert.DeserializeObject<tipoConsulta>(responseData);
                    TempData["mensaje"] = "El tipoConsulta se ha actualizado correctamente.";
                    return RedirectToAction("ListadotipoConsulta", "MantenimientoTipoConsulta");
                }
                else
                {
                    // Manejo de error si la solicitud no fue exitosa
                    TempData["mensaje"] = "Error al actualizar el Usuario: " + response.ReasonPhrase;
                    return RedirectToAction("ListadotipoConsulta", "MantenimientoTipoConsulta");
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                TempData["mensaje"] = "Error al procesar la solicitud: " + ex.Message;
                return RedirectToAction("ListadotipoConsulta", "MantenimientoTipoConsulta");
            }
        }
        //metodo para insertar
        [HttpPost]
        public async Task<IActionResult> ActualizartipoConsulta(tipoConsulta datos)
        {
            try
            {

                var json = JsonConvert.SerializeObject(datos);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Realiza la solicitud PUT al endpoint correcto
                var response = await _httpClient.PutAsync("tipoConsulta", content);

                string redirectUrl = $"https://localhost:7162/TicketAPI/AbrirTicket?id={datos.idTipoConsulta}";

                if (response.IsSuccessStatusCode)
                {
                    TempData["mensaje"] = "El ticket se ha actualizado correctamente.";
                    TempData["mensajeTipo"] = "success";
                    return RedirectToAction("ListadotipoConsulta", "MantenimientoTipoConsulta");
                }
                else
                {
                    TempData["mensaje"] = "Error al actualizar el ticket: " + response.ReasonPhrase;
                    TempData["mensajeTipo"] = "error";
                    return RedirectToAction("ListadotipoConsulta", "MantenimientoTipoConsulta");
                }
            }
            catch (Exception ex)
            {
                TempData["mensaje"] = "Error al procesar la solicitud: " + ex.Message;
                TempData["mensajeTipo"] = "error";
                return RedirectToAction("ListadotipoConsulta", "MantenimientoTipoConsulta");
            }
        }
        //abrir 
        public async Task<IActionResult> actualizartipoConsulta(int id)
        {
            // Llamar al endpoint con el ID del ticket

            var rol = "Receptor";

            HttpResponseMessage response = await _httpClient.GetAsync($"tipoConsulta/{id}");
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var tipoConsulta = JsonConvert.DeserializeObject<tipoConsulta>(responseData);
                ViewBag.tipoConsulta = tipoConsulta;
            }
            else
            {
                ViewBag.tipoConsulta = null;
            }
            return View();
        }


        public async Task<IActionResult> CreartipoConsulta()
        {

            return View();
        }


        public async Task<IActionResult> EliminarDatostipoConsulta(int id)
        {
            // Llamar al endpoint con el ID del ticket

            var rol = "Receptor";

            HttpResponseMessage response = await _httpClient.GetAsync($"tipoConsulta/{id}");
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var tipoConsulta = JsonConvert.DeserializeObject<tipoConsulta>(responseData);
                ViewBag.tipoConsulta = tipoConsulta;
            }
            else
            {
                ViewBag.tipoConsulta = null;
            }
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> EliminarTipoConsulta(int id)
        {
            try
            {
                // Realiza la solicitud DELETE al endpoint correcto
                var response = await _httpClient.DeleteAsync($"tipoConsulta/{id}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["mensaje"] = " tipoConsulta se ha eliminado correctamente.";
                    TempData["mensajeTipo"] = "success";
                }
                else
                {
                    TempData["mensaje"] = "Error al eliminar la tipoConsulta: " + response.ReasonPhrase;
                    TempData["mensajeTipo"] = "error";
                }
            }
            catch (Exception ex)
            {
                TempData["mensaje"] = "Error al procesar la solicitud: " + ex.Message;
                TempData["mensajeTipo"] = "error";
            }

            // Redirige a la acción de listado de tipoConsulta
            return RedirectToAction("ListadotipoConsulta", "MantenimientoTipoConsulta");
        }




    }
}
