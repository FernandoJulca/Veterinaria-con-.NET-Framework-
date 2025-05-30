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
    public class GestionRaza
    {
        private readonly RazaDTO _Raza;

        public GestionRaza()
        {
            _Raza = new RazaDTO();
        }
        public async Task<string> Actualizar(Raza reg)
        {
            return await _Raza.Actualizar(reg);
        }

        public async Task<string> Agregar(Raza reg)
        {
            return await _Raza.Agregar(reg);
        }

        public async Task<Raza> Buscar(int id)
        {
            return await _Raza.Buscar(id);
        }

        public async Task<string> Eliminar(int id)
        {
            return await _Raza.Eliminar(id);
        }
        public async Task<List<Raza>> ListarRazaCliente()
        {
            return (List<Raza>)await _Raza.Listar();
        }

    }
}
