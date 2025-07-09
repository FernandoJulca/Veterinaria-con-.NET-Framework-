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

        public async Task<string> RegistrarCliente(Cliente c){
            return await clienteDTO.RegistrarCliente(c);
        }

        public async Task<IEnumerable<Cliente>> Listar()
        {
            return await clienteDTO.Listar();
        }

        public async Task<List<Venta>> HistorialCompras(int idCliente)
        {
            return await clienteDTO.HistorialCompras(idCliente);
        }

        public async Task<Cliente> ObtenerCliente(int idCliente)
        {
            return await clienteDTO.ObtenerCliente(idCliente);
        }

        public async Task<List<Reserva>> Historial_Citas(int idCliente)
        {
            return await clienteDTO.Historial_Citas(idCliente);
        }
    }
}
