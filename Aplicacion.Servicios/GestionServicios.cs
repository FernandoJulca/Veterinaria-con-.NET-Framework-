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

        public async Task<string> Actualizar(Servicio reg)
        {
            return await servicioDTO.Actualizar(reg);
        }

        public async Task<string> Agregar(Servicio reg)
        {
            return await servicioDTO.Agregar(reg);
        }

        public async Task<Servicio> Buscar(int id)
        {
            return await servicioDTO.Buscar(id);
        }

        public async Task<string> Eliminar(int id)
        {
            return await servicioDTO.Eliminar(id);
        }
    }
}
