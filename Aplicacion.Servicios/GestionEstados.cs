using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Dominio.Entidad.Entidad;
using Infraestructura.Data;

namespace Aplicacion.Servicios
{
    public class GestionEstados
    {
        private readonly EstadoDTO _estado;
        public GestionEstados()
        {
            _estado = new EstadoDTO();
        }

        public async Task<SelectList> Listar()
        {
            IEnumerable<Estado> estados = await _estado.Listar();
            return new SelectList(estados, "IdEstado", "Descripcion");
        }

        public async Task<SelectList> Listar(Producto reg)
        {
            IEnumerable<Estado> estados = await _estado.Listar();
            return new SelectList(estados, "IdEstado", "Descripcion", reg.IdEstado);
        }
    }
}
