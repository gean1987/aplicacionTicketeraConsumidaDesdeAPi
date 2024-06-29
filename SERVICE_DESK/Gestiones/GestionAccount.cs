using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.DirectoryServices.AccountManagement;
using SERVICE_DESK.Models;
using System.Data.SqlClient;
using System.Data;
using SERVICE_DESK.Interfaces;
using Microsoft.AspNetCore.Http;


using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

using System.Text.Json;





namespace SERVICE_DESK.Gestiones
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }

    public class GestionAccount
    {
        private readonly string _cadena;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GestionAccount(string cadena, IHttpContextAccessor httpContextAccessor)
        {
            _cadena = cadena;
            _httpContextAccessor = httpContextAccessor;
        }

        public Usuario EstaEnBaseDeDatos(string correo)
        {
            var usuario = new Usuario();

            using (var conexion = new SqlConnection(_cadena))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("usp_Validar_Acceso", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@correo_usuario", correo);

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            usuario = new Usuario()
                            {
                                IdUsuario = dr.GetInt32(0),
                                nombre = dr.GetString(1),
                                Rol = dr.GetString(2),
                                correo = dr.GetString(3)
                            };
                        }
                    }
                }
            }
            return usuario;
        }

        public void IniciarSesion(Usuario usuario)
        {
            var session = _httpContextAccessor.HttpContext.Session;

            session.Set("usuario", usuario);
            session.Set("filtroEstadoTicketGeneral", new List<int>() { 1 });
            session.Set("filtroEstadoTicketPersonal", new List<int>() { 1 });
            // Modificaciones para los filtros
            session.Set("filtroFechaTicketPersonal", new List<int>() { 1 });
            session.Set("fechaInicioTicketPersonal", new DateTime(2024, 1, 1));
            session.Set("fechaFinTicketPersonal", DateTime.Today);
            //
            session.Set("filtroResponsableTicket", new List<int>() { 0 });
            session.SetInt32("minutosSesionRestantes", 560);
           

            // Configura el tiempo de espera de la sesión
            session.SetInt32("timeout", 20 * 60); // 20 minutos
        }
        public void CerrarSesion(ISession session)
        {
            session.Clear();
        }

 
    }
}
