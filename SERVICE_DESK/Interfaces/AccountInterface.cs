using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SERVICE_DESK.Models;

namespace SERVICE_DESK.Interfaces
{
    interface AccountInterface
    {
        bool EstaEnActiveDirectory(string correo, string clave);

        Usuario EstaEnBaseDeDatos(string correo);

        void IniciarSesion(Usuario usuario);

        void CerrarSesion();

    }
}
