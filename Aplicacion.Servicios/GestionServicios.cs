using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidad.Entidad;
using Infraestructura.Data;

namespace Aplicacion.Servicios
{
    public class GestionServicios
    {
        private readonly ServicioDTO servicioDTO;
        public GestionServicios()
        {
            servicioDTO = new ServicioDTO();
        }

        public async Task<IEnumerable<Servicio>> Listar()
        {
            return await servicioDTO.Listar();
        }
    }
}
