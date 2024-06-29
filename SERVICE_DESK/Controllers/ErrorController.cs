using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SERVICE_DESK.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult OperacionSinAutorizacion()
        {
            return View();
        }
    }
}