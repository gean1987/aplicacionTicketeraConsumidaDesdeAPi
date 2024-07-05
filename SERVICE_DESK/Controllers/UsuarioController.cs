using Microsoft.AspNetCore.Mvc;

namespace SERVICE_DESK.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Listado()
        {
            return View();
        }
    }
}
