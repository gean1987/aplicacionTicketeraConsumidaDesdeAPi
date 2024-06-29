using SERVICE_DESK.Models;
namespace SERVICE_DESK.Datos
{
    public class DA_Usuario
    {

        public List<Usuario> ListaUsuario()
        {

            return new List<Usuario>
            {
                new Usuario{ nombre ="jose", correo = "administrador@gmail.com" , Rol ="Administrador" },
                new Usuario{ nombre ="maria", correo = "supervisor@gmail.com", Rol = "Supervisor"} ,
                new Usuario{ nombre ="juan", correo = "empleado@gmail.com", Rol = "Empleado"} ,
                new Usuario{ nombre ="oscar", correo = "superempleado@gmail.com", Rol = "Empleado" }

            };

        }

        

    }
}
