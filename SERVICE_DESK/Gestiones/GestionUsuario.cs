using SERVICE_DESK.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SERVICE_DESK.Datos;
using Microsoft.Extensions.Configuration;
using System.DirectoryServices.AccountManagement;



namespace SERVICE_DESK.Gestiones
{
    public class GestionUsuario 
    {

        private readonly Usuario _usuarioSesion;
        private readonly IConfiguration _configuration;
        private readonly Conexion cn = new Conexion();

        readonly string _cadena;
        public GestionUsuario(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _usuarioSesion = httpContextAccessor.HttpContext.Session.GetString("usuario") != null
                ? Newtonsoft.Json.JsonConvert.DeserializeObject<Usuario>(httpContextAccessor.HttpContext.Session.GetString("usuario"))
                : new Usuario();
            _configuration = configuration;
        }

        public GestionUsuario()
        {
            _cadena = cn.getCadenaSQL();

        }








    }
}
