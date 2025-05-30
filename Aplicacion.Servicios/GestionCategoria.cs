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

        public async Task<List<Categoria>> ListarCategoriaCliente()
        {
            return (List<Categoria>)await _categoria.Listar();
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

        public async Task<string> Actualizar(Categoria reg)
        {
            return await _categoria.Actualizar(reg);
        }

        public async Task<string> Agregar(Categoria reg)
        {
            return await _categoria.Agregar(reg);
        }

        public async Task<Categoria> Buscar(int id)
        {
            return await _categoria.Buscar(id);
        }

        public async Task<string> Eliminar(int id)
        {
            return await _categoria.Eliminar(id);
        }
    }
}
