using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SERVICE_DESK.Enums;
using SERVICE_DESK.Enums;
using SERVICE_DESK.Gestiones;
using SERVICE_DESK.Models;

namespace SERVICE_DESK.Controllers
{
    public class AccountController : Controller
    {
        private readonly GestionAccount _gestionAccount;
        private readonly GestionUsuario _gestionUsuario;

        public AccountController(GestionAccount gestionAccount, GestionUsuario gestionUsuario)
        {
            _gestionAccount = gestionAccount;
            _gestionUsuario = gestionUsuario;
        }

        public class CapthaResponseViewModel
        {
            public bool Success { get; set; }
            public IEnumerable<string> ErrorCodes { get; set; }
            public DateTime ChallengeTime { get; set; }
            public string HostName { get; set; }
            public double Score { get; set; }
            public string Action { get; set; }
        }

        public IActionResult Login(string rutaOrigen = "")
        {
            ViewBag.rutaOrigen = rutaOrigen;
            return View();
        }

      
        [HttpPost]
        public void ExtenderTiempoSesion()
        {
            int minutosSesionRestantes = HttpContext.Session.GetInt32("minutosSesionRestantes") ?? 0;
            HttpContext.Session.SetInt32("minutosSesionRestantes", minutosSesionRestantes + 20);
        }

    }
}
