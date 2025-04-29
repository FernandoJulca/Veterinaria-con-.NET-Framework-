using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Dominio.Entidad.Abstraccion;
using Dominio.Entidad.Entidad;
using Infraestructura.Data;
namespace ProyectoVeterinaria.Controllers
{
    public class ProductoController : Controller
    {
        ProductoDTO _producto = new ProductoDTO(); //no tan recomendado
        CategoriaDTO _categoria = new CategoriaDTO();
      
        // GET: Producto
        public async  Task<ActionResult> ListaProductos()
        {
            var productos = await _producto.Listar();
            var categorias = await _categoria.Listar();
            

            return View(productos);
        }

        [HttpGet] 
        public async Task<ActionResult> Create()
        {
            
            var categorias = await _categoria.Listar();

            ViewBag.categorias = new SelectList(categorias, "IdCategoria", "NombreCategoria");
            return View(new Producto());
        }

        [HttpPost]
        public async Task<ActionResult> Create(Producto reg)
        {
            var categorias = await _categoria.Listar();
            ViewBag.mensaje = await _producto.Agregar(reg);
            ViewBag.categorias = new SelectList(categorias, "IdCategoria", "NombreCategoria", reg.IdCategoria);

            return View(reg);
        }
    }
}