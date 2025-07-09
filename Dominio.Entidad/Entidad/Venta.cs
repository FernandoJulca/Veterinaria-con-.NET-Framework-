using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidad.Entidad
{
    public class Venta
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }
        public string ApellidoCliente { get; set; }
        public List<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
        /*Adicional para el dashboard*/
        public string Mes { get; set; }
        public decimal TotalMensual { get; set; }
    }
}
