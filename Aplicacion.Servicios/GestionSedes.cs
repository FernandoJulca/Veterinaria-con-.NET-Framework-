using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidad.Abstraccion;
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

        public async Task<string> Actualizar(Sede reg)
        {
            return await sedeDTO.Actualizar(reg);
        }

        public async Task<string> Agregar(Sede reg)
        {
            return await sedeDTO.Agregar(reg);
        }

        public async Task<Sede> Buscar(int id)
        {
            return await sedeDTO.Buscar(id);
        }

        public async Task<string> Eliminar(int id)
        {
            return await sedeDTO.Eliminar(id);
        }

    }
}
