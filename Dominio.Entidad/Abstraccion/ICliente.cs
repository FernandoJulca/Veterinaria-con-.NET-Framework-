using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidad.Entidad;
using Dominio.Repositorio;

namespace Dominio.Entidad.Abstraccion
{
    public interface ICliente : IRepositorioGET<Cliente>
    {
    }
}
