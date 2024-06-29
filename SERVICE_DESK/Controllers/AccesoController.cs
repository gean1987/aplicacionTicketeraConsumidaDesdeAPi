using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using SERVICE_DESK.Models; // Asegúrate de tener el espacio de nombres correcto para tu modelo de usuario
using Newtonsoft.Json;

namespace SERVICE_DESK.Controllers
{
    public class AccesoController : Controller
    {
        private readonly HttpClient _httpClient;

        public AccesoController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("http://localhost:8081/"); // Cambia la URL base según tu configuración
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string correo, string contrasena)
        {
            // Construir la URL para llamar a la API
            string apiUrl = $"usuario/{correo}/{contrasena}";

            // Llamar a la API usando HttpClient
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                // Leer el contenido de la respuesta como string
                string responseData = await response.Content.ReadAsStringAsync();

                // Deserializar el JSON a una lista de objetos Usuario
                var usuario = JsonConvert.DeserializeObject<List<Usuario>>(responseData);

                if (usuario != null && usuario.Count > 0)
                {
                    // Procesar la lógica de autenticación como lo hacías antes
                    string rolUsuario;

                    if (usuario[0].Rol != null && usuario[0].Rol.Contains("Administrador"))
                    {
                        rolUsuario = "Administrador";
                    }
                    else if (usuario[0].Rol != null && usuario[0].Rol.Contains("Emisor"))
                    {
                        rolUsuario = "Emisor";
                    }
                    else if (usuario[0].Rol != null && usuario[0].Rol.Contains("Receptor"))
                    {
                        rolUsuario = "Receptor";
                    }
                    else
                    {
                        rolUsuario = "Invitado";
                    }

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario[0].nombre),
                        new Claim(ClaimTypes.NameIdentifier, usuario[0].IdUsuario.ToString()),
                        new Claim(ClaimTypes.Email, usuario[0].correo),
                        new Claim(ClaimTypes.Role, usuario[0].Rol)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    HttpContext.Session.SetString("Usuario_NombresUsuario", usuario[0].nombre);
                    HttpContext.Session.SetString("Usuario_Rol", rolUsuario);
                    HttpContext.Session.SetString("emailUsuario", usuario[0].correo);

                    // Redirige al usuario según su rol
                    if (rolUsuario == "Receptor")
                    {
                        return RedirectToAction("MisTicketsAsignados", "TicketAPI");
                    }
                    else if (rolUsuario == "Administrador")
                    {
                        return RedirectToAction("ListadoGeneral", "TicketAPI");
                    }
                    else if (rolUsuario == "Emisor")
                    {
                        return RedirectToAction("MisTickets", "TicketAPI");
                    }
                    else
                    {
                        return RedirectToAction("Privacy", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("Privacy", "Home");
                }
            }
            else
            {
                return RedirectToAction("Privacy", "Home");
            }
        }

        public async Task<IActionResult> Salir()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
    }
}
