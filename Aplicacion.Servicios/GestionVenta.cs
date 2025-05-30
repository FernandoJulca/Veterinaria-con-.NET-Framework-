using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidad.Entidad;
using Infraestructura.Data;

namespace Aplicacion.Servicios
{
    public class GestionVenta
    {
        private readonly VentasDTO _Venta;

        public GestionVenta()
        {
            _Venta = new VentasDTO();
        }
        public async Task<IEnumerable<Venta>> ListarPorMes()
        {
            return await _Venta.ListarPorMes();
        }
    }
}
