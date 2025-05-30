using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidad.Abstraccion;
using System.Web.Mvc;
using Dominio.Entidad.Entidad;
using Infraestructura.Data;

namespace Aplicacion.Servicios
{
    public class GestionEspecialidad
    {
        private readonly EspecialidadDTO _Especialidad;

        public GestionEspecialidad()
        {
            _Especialidad = new EspecialidadDTO();
        }
        public async Task<string> Actualizar(Especialidad reg)
        {
            return await _Especialidad.Actualizar(reg);
        }

        public async Task<string> Agregar(Especialidad reg)
        {
            return await _Especialidad.Agregar(reg);
        }

        public async Task<Especialidad> Buscar(int id)
        {
            return await _Especialidad.Buscar(id);
        }

        public async Task<string> Eliminar(int id)
        {
            return await _Especialidad.Eliminar(id);
        }
        public async Task<List<Especialidad>> ListarEspecialidadCliente()
        {
            return (List<Especialidad>)await _Especialidad.Listar();
        }
        public async Task<SelectList> ListarEspecialidadGet()
        {
            IEnumerable<Especialidad> raza = await _Especialidad.Listar();
            return new SelectList(raza, "IdEspecialidad", "NombreEspecialidad");
        }

        public async Task<SelectList> ListarEspecialidadPost(Veterinario reg)
        {
            IEnumerable<Especialidad> raza = await _Especialidad.Listar();
            return new SelectList(raza, "IdEspecialidad", "NombreEspecialidad", reg.IdEspecialidad);
        }
    }
}
