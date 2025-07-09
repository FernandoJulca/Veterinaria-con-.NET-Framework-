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

        public async Task<string> AgregarVenta(int idCliente, List<Carro> carrito)
        {
            return await _Venta.AgregarVenta(idCliente,carrito);
        }

        public async Task<List<Venta>> HistorialComprasAdmin()
        {
            return await _Venta.HistorialComprasAdmin();
        }
    }
}
