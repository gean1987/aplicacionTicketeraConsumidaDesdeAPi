using Microsoft.AspNetCore.Mvc;
using SERVICE_DESK.Models;
using SERVICE_DESK.Controllers;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Claims;
using ClosedXML.Excel;
using HtmlAgilityPack;
using SERVICE_DESK.Enums;
using SERVICE_DESK.Gestiones;
using SERVICE_DESK.Models;

using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace SERVICE_DESK.Controllers
{
    public class HomeController : Controller
    {
        private readonly GestionUsuario gestionUsuario;
   
        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;

            gestionUsuario = new GestionUsuario();
        }


        public async Task<IActionResult> CuentaCreada()
        {


            return View();
        }

        public IActionResult Index()
        {
            ViewBag.Email = HttpContext.Session.GetString("emailUsuarioNoEncontrado"); // Obtener el correo electrónico de la sesión y asignarlo a ViewBag


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}