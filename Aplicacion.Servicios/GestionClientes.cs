using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidad.Entidad;
using Infraestructura.Data;

namespace Aplicacion.Servicios
{
    public class GestionClientes
    {
        private readonly ClienteDTO clienteDTO;
        public GestionClientes()
        {
            clienteDTO = new ClienteDTO();
        }
        public async Task<Cliente> IniciarSesion(string correo, string contrasenia)
        {
            return await clienteDTO.IniciarSesion(correo, contrasenia);
        }
    }
}
