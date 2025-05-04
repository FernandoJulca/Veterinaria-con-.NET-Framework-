using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidad.Entidad;
using Infraestructura.Data;

namespace Aplicacion.Servicios
{
    public class GestionVeterinarios
    {
        private readonly VeterinariaDTO _veterinarioDTO;
        public GestionVeterinarios()
        {
            _veterinarioDTO = new VeterinariaDTO();
        }
        public async Task<IEnumerable<Veterinario>> Listar()
        {
            return await _veterinarioDTO.Listar();
        }
    }
}
