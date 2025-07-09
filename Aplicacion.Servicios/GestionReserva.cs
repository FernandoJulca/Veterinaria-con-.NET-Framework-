using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidad.Entidad;
using Infraestructura.Data;

namespace Aplicacion.Servicios
{
    public class GestionReserva
    {
        private readonly ReservaDTO reserva = new ReservaDTO();

        public async Task<string> crearReserva(Reserva r)
        {
            return await reserva.crearReserva(r);
        }

        public async Task<List<Reserva>> listaHistorialAdmin()
        {
            return await reserva.listaHistorialAdmin();
        }

    }
}
