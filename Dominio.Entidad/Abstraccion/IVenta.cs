using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidad.Entidad;

namespace Dominio.Entidad.Abstraccion
{
    public interface IVenta
    {
       Task<IEnumerable<Venta>> ListarPorMes();
    }
}
