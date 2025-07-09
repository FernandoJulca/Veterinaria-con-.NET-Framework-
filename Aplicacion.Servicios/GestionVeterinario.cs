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
    public class GestionVeterinario
    {
        private readonly VeterinariaDTO __veterinarioDTO;
        public GestionVeterinario()
        {
            __veterinarioDTO = new VeterinariaDTO();
        }
        public async Task<IEnumerable<Veterinario>> Listar()
        {
            return await __veterinarioDTO.Listar();
        }

        public async Task<string> Actualizar(Veterinario reg)
        {
            return await __veterinarioDTO.Actualizar(reg);
        }

        public async Task<string> Agregar(Veterinario reg)
        {
            return await __veterinarioDTO.Agregar(reg);
        }

        public async Task<Veterinario> Buscar(int id)
        {
            return await __veterinarioDTO.Buscar(id);
        }

        public async Task<string> Eliminar(int id)
        {
            return await __veterinarioDTO.Eliminar(id);
        }
    }
}
