using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Aplicacion.Servicios;
using Dominio.Entidad.Abstraccion;
using Dominio.Entidad.Entidad;
using Infraestructura.Data;
namespace ProyectoVeterinaria.Controllers
{
    public class ProductoController : Controller
    {
        private readonly GestionProductos _gestionProductos = new GestionProductos();
        private readonly GestionEstados _gestionEstados = new GestionEstados();
        private readonly GestionCategoria _gestionCategoria = new GestionCategoria();

        // GET: Producto
        public async Task<ActionResult> Productos()
        {
            ViewBag.categorias = await _gestionCategoria.ListarCategoriaCliente();
            return View();
        }

        public async Task<JsonResult> ListarProductosPorCategoria(int id, int page = 1, int pageSize = 12)
        {
            List<Dominio.Entidad.Entidad.ListadoProductos> productos;

            if (id == 0)
            {
                productos = await _gestionProductos.ObtenerListadoProductos();
            }
            else
            {
                productos = await _gestionProductos.ListarProductosPorCategoria(id);
            }

            var totalProductos = productos.Count;
            var totalPaginas = (int)Math.Ceiling((double)totalProductos / pageSize);

            var productosPaginados = productos
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return Json(new
            {
                productos = productosPaginados,
                totalPaginas,
                paginaActual = page
            }, JsonRequestBehavior.AllowGet);
        }
   
    }
}