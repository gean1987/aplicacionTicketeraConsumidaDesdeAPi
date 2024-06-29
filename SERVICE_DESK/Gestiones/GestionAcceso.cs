using SERVICE_DESK.Datos;
using SERVICE_DESK.Models;
using System.Data;
using System.Data.SqlClient;
using System.Data;

namespace SERVICE_DESK.Gestiones
{
    public class GestionAcceso
    {

        private readonly string _cadena;

        private readonly Usuario _usuarioSesion;

        public GestionAcceso(IHttpContextAccessor httpContextAccessor)
        {
            _usuarioSesion = httpContextAccessor.HttpContext.Session.GetString("usuario") != null
                ? Newtonsoft.Json.JsonConvert.DeserializeObject<Usuario>(httpContextAccessor.HttpContext.Session.GetString("usuario"))
                : new Usuario();
        }

        public GestionAcceso()
        {
        }










        public List<Usuario> Listar()
        {

            var oLista = new List<Usuario>();

            var cn = new Conexion();

            using (var conexion = new SqlConnection(cn.getCadenaSQL()))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ObtenerInformacionUsuarios", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = cmd.ExecuteReader())
                {

                    while (dr.Read())
                    {
                        oLista.Add(new Usuario()
                        {
                            IdUsuario = Convert.ToInt32(dr["id_usuario"]),
                            nombre = dr["Nombre"].ToString(),
                            correo = dr["mail"].ToString(),
                            Rol = dr["Rol"].ToString()
                        });

                    }
                }
            }

            return oLista;
        }







        public Usuario ValidarUsuario(string _correo)
        {

            return Listar().Where(item => item.correo == _correo).FirstOrDefault();

        }



    }
}
