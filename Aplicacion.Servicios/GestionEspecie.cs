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
    public class GestionEspecie
    {
        private readonly EspecieDTO _Especie;

        public GestionEspecie()
        {
            _Especie = new EspecieDTO();
        }
        public async Task<string> Actualizar(Especie reg)
        {
            return await _Especie.Actualizar(reg);
        }

        public async Task<string> Agregar(Especie reg)
        {
            return await _Especie.Agregar(reg);
        }

        public async Task<Especie> Buscar(int id)
        {
            return await _Especie.Buscar(id);
        }

        public async Task<string> Eliminar(int id)
        {
            return await _Especie.Eliminar(id);
        }
        public async Task<List<Especie>> ListarEspecieCliente()
        {
            return (List<Especie>)await _Especie.Listar();
        }

        public async Task<SelectList> ListarEspecieGet()
        {
            IEnumerable<Especie> raza = await _Especie.Listar();
            return new SelectList(raza, "IdEspecie", "NombreEspecie");
        }

    }
}
