using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidad.Entidad;
using Infraestructura.Data;

namespace Aplicacion.Servicios
{
    public class GestionSedes
    {
        private readonly SedeDTO sedeDTO;
        public GestionSedes()
        {
            sedeDTO = new SedeDTO();
        }

        public async Task<IEnumerable<Sede>> Listar()
        {
            return await sedeDTO.Listar();
        }
    }
}
