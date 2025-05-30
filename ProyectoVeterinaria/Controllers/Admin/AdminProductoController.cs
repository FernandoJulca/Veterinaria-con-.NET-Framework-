using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Aplicacion.Servicios;
using Dominio.Entidad.Entidad;

namespace ProyectoVeterinaria.Controllers.Admin
{
    public class AdminProductoController : Controller
    {
        private readonly GestionProductos _gestionProductos = new GestionProductos();
        private readonly GestionEstados _gestionEstados = new GestionEstados();
        private readonly GestionCategoria _gestionCategoria = new GestionCategoria();

        public async Task<ActionResult> ListaProductos(int page = 1, int pageSize = 10)
        {
            var todosLosProductos = await _gestionProductos.ObtenerListadoProductos();

            var totalItems = todosLosProductos.Count();
            var productosPaginados = todosLosProductos
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return View("~/Views/Admin/AdminProducto/ListaProductos.cshtml",productosPaginados);
        }

        //CREATE
        public async Task<ActionResult> Create()
        {
            ViewBag.categorias = await _gestionCategoria.ListarCategoria();

            return View("~/Views/Admin/AdminProducto/Create.cshtml",new Producto());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Producto reg)
        {
            if (!string.IsNullOrEmpty(reg.ImagenBase64))
            {
                try
                {
                    reg.Imagen = Convert.FromBase64String(reg.ImagenBase64);
                }
                catch (FormatException)
                {
                    ModelState.AddModelError("ImagenBase64", "Formato de imagen inválido.");
                }
            }

            if (ModelState.IsValid)
            {
                TempData["GoodMessage"] = await _gestionProductos.AgregarProducto(reg);
                return RedirectToAction("ListaProductos");
            }

            ViewBag.categorias = await _gestionCategoria.ListarCategoriaPost(reg);
            return View("~/Views/Admin/AdminProducto/Create.cshtml",reg);
        }


        //EDIT
        public async Task<ActionResult> Edit(int id)
        {
            Producto reg = await _gestionProductos.BuscarProducto(id);
            ViewBag.categorias = await _gestionCategoria.ListarCategoriaPost(reg);
            ViewBag.estados = await _gestionEstados.Listar(reg);
            return View("~/Views/Admin/AdminProducto/Edit.cshtml", reg);

        }

        [HttpPost]
        public async Task<ActionResult> Edit(Producto reg)
        {
            TempData["GoodMessage"] = await _gestionProductos.ActualizaProducto(reg);
            ViewBag.categorias = await _gestionCategoria.ListarCategoriaPost(reg);
            ViewBag.estados = await _gestionEstados.Listar(reg);
            return RedirectToAction("ListaProductos");
        }

        //DELETE
        public async Task<ActionResult> Delete(int? id = null)
        {
            var producto = await _gestionProductos.BuscarProducto(id.Value);

            return View("~/Views/Admin/AdminProducto/Delete.cshtml", producto);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            TempData["GoodMessage"] = await _gestionProductos.EliminarProducto(id);
            return RedirectToAction("ListaProductos");
        }

        // DETAILS
        public async Task<ActionResult> Details(int? id = null)
        {
            var producto = await _gestionProductos.BuscarProducto(id.Value);
            return View("~/Views/Admin/AdminProducto/Details.cshtml", producto);
        }
    }
}