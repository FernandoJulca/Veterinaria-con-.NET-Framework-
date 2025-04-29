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
    public class GestionCategoria
    {
        private readonly CategoriaDTO _categoria;

        public GestionCategoria()
        {
            _categoria = new CategoriaDTO();
        }

        public async Task<SelectList> ListarCategoria()
        {
            IEnumerable<Categoria> categorias = await _categoria.Listar();
            return new SelectList(categorias, "IdCategoria", "NombreCategoria");
        }

        public async Task<SelectList> ListarCategoriaPost(Producto reg)
        {
            IEnumerable<Categoria> categorias = await _categoria.Listar();
            return new SelectList(categorias, "IdCategoria", "NombreCategoria",reg.IdCategoria);
        }
    }
}
