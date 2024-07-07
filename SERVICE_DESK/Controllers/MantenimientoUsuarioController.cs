using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SERVICE_DESK.Models;
using System.Security.Claims;
using System.Text;
namespace SERVICE_DESK.Controllers
{
    public class MantenimientoUsuarioController : Controller
    {

        private readonly HttpClient _httpClient;

        public MantenimientoUsuarioController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:8081/"); // Cambia la URL base según tu configuración
        }

        public async Task<IActionResult> ListadoUsuarios()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("usuario");
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Usuario>>(responseData);
                ViewBag.usuarios = data;
            }
            else
            {
                ViewBag.usuarios = new List<Usuario>();
            }
        
            return View();
        }



        //metodo para crear
        [HttpPost]
        public async Task<IActionResult> AgregarUsuario(Usuario datos)
        {

            try
            {
                var json = JsonConvert.SerializeObject(datos);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("usuario", content);

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var createdTicket = JsonConvert.DeserializeObject<Ticket>(responseData);
                    TempData["mensaje"] = "El Usuario se ha actualizado correctamente.";
                    return RedirectToAction("ListadoUsuarios", "MantenimientoUsuario");
                }
                else
                {
                    // Manejo de error si la solicitud no fue exitosa
                    TempData["mensaje"] = "Error al actualizar el Usuario: " + response.ReasonPhrase;
                    return RedirectToAction("ListadoUsuarios", "MantenimientoUsuario");
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                TempData["mensaje"] = "Error al procesar la solicitud: " + ex.Message;
                return RedirectToAction("ListadoUsuarios", "MantenimientoUsuario");
            }
        }
        //metodo para insertar
        [HttpPost]
        public async Task<IActionResult> ActualizarUsuario(Usuario datos)
        {
            try
            {
               
                var json = JsonConvert.SerializeObject(datos);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Realiza la solicitud PUT al endpoint correcto
                var response = await _httpClient.PutAsync("usuario", content);

                string redirectUrl = $"https://localhost:7162/TicketAPI/AbrirTicket?id={datos.idUsuario}";

                if (response.IsSuccessStatusCode)
                {
                    TempData["mensaje"] = "El ticket se ha actualizado correctamente.";
                    TempData["mensajeTipo"] = "success";
                    return RedirectToAction("ListadoUsuarios", "MantenimientoUsuario");
                }
                else
                {
                    TempData["mensaje"] = "Error al actualizar el ticket: " + response.ReasonPhrase;
                    TempData["mensajeTipo"] = "error";
                    return RedirectToAction("ListadoUsuarios", "MantenimientoUsuario");
                }
            }
            catch (Exception ex)
            {
                TempData["mensaje"] = "Error al procesar la solicitud: " + ex.Message;
                TempData["mensajeTipo"] = "error";
                return RedirectToAction("ListadoUsuarios", "MantenimientoUsuario");
            }
        }
        //abrir Ticket
        public async Task<IActionResult> actualizarUsuario(int id)
        {
            // Llamar al endpoint con el ID del ticket

            var rol = "Receptor";

            HttpResponseMessage response = await _httpClient.GetAsync($"usuario/{id}");
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var usuarios = JsonConvert.DeserializeObject<Usuario>(responseData);
                ViewBag.usuarios = usuarios;
            }
            else
            {
                ViewBag.usuarios = null;
            }
            return View();
        }


        public async Task<IActionResult> CrearUsuario()
        {
           
            return View();
        }


        public async Task<IActionResult> EliminarDatosUsuario(int id)
        {
            // Llamar al endpoint con el ID del ticket

            var rol = "Receptor";

            HttpResponseMessage response = await _httpClient.GetAsync($"usuario/{id}");
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var usuarios = JsonConvert.DeserializeObject<Usuario>(responseData);
                ViewBag.usuarios = usuarios;
            }
            else
            {
                ViewBag.usuarios = null;
            }
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            try
            {
                // Realiza la solicitud DELETE al endpoint correcto
                var response = await _httpClient.DeleteAsync($"usuario/{id}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["mensaje"] = "El usuario se ha eliminado correctamente.";
                    TempData["mensajeTipo"] = "success";
                }
                else
                {
                    TempData["mensaje"] = "Error al eliminar el usuario: " + response.ReasonPhrase;
                    TempData["mensajeTipo"] = "error";
                }
            }
            catch (Exception ex)
            {
                TempData["mensaje"] = "Error al procesar la solicitud: " + ex.Message;
                TempData["mensajeTipo"] = "error";
            }

            // Redirige a la acción de listado de usuarios
            return RedirectToAction("ListadoUsuarios", "MantenimientoUsuario");
        }



    }
}
