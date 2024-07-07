using SERVICE_DESK.Models;
namespace SERVICE_DESK.Datos
{
    public class DA_Usuario
    {

        public List<Usuario> ListaUsuario()
        {

            return new List<Usuario>
            {
                new Usuario{ nombre ="jose", correo = "administrador@gmail.com" , rol ="Administrador" },
                new Usuario{ nombre ="maria", correo = "supervisor@gmail.com", rol = "Supervisor"} ,
                new Usuario{ nombre ="juan", correo = "empleado@gmail.com", rol = "Empleado"} ,
                new Usuario{ nombre ="oscar", correo = "superempleado@gmail.com", rol = "Empleado" }

            };

        }

        

    }
}
