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
        
        // GET: Producto
        public async  Task<ActionResult> ListaProductos()
        {

            var lista = await _gestionProductos.ObtenerListadoProductos();
            return View(lista);
        }
        public async Task<ActionResult> ListaProductostwo()
        {

            var lista = await _gestionProductos.ObtenerListadoProductos();
            return View(lista);
        }
        //CREATE
        private readonly GestionCategoria _gestionCategoria = new GestionCategoria();
        public async Task<ActionResult> Create()
        {
            ViewBag.categorias = await _gestionCategoria.ListarCategoria();
           
            return View(new Producto());
        }

      
        [HttpPost]
        
        public async Task<ActionResult> Create(Producto reg)
        {


            
            ViewBag.mensaje = await _gestionProductos.AgregarProducto(reg);
            ViewBag.categorias = await _gestionCategoria.ListarCategoriaPost(reg);

            return View(reg);
        }

        //EDIT
        public async Task<ActionResult> Edit(int id)
        {
            Producto reg = await _gestionProductos.BuscarProducto(id);
            ViewBag.categorias = await _gestionCategoria.ListarCategoriaPost(reg);
            return View(reg);

        }

        [HttpPost]
        public async Task<ActionResult> Edit(Producto reg)
        {
            ViewBag.mensaje = await _gestionProductos.ActualizaProducto(reg);
            ViewBag.categorias = await _gestionCategoria.ListarCategoriaPost(reg);
            return View(reg);
        }

        //DELETE
        public async Task<ActionResult> Delete(int ? id = null)
        {
            var producto = await _gestionProductos.BuscarProducto(id.Value);   
            
            return View("Delete",producto);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            ViewBag.mensaje = await _gestionProductos.EliminarProducto(id);
            var producto = await _gestionProductos.BuscarProducto(id);
            return View("Delete",producto);
        }
    }
}