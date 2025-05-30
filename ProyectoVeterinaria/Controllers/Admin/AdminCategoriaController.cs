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
    public class AdminCategoriaController : Controller
    {
        private readonly GestionCategoria _gestionCategoria = new GestionCategoria();

        public async Task<ActionResult> ListaCategoria(int page = 1, int pageSize = 10)
        {
            var todosLosCategorias = await _gestionCategoria.ListarCategoriaCliente();

            var totalItems = todosLosCategorias.Count();
            var CategoriasPaginados = todosLosCategorias
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return View("~/Views/Admin/AdminCategoria/ListaCategoria.cshtml", CategoriasPaginados);
        }

        //CREATE
        public async Task<ActionResult> Create()
        {
            return View("~/Views/Admin/AdminCategoria/Create.cshtml", new Categoria());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Categoria reg)
        {
            if (ModelState.IsValid)
            {
                TempData["GoodMessage"] = await _gestionCategoria.Agregar(reg);
                return RedirectToAction("ListaCategoria");

            }
            return View("~/Views/Admin/AdminCategoria/Create.cshtml", reg);
        }
        //EDIT
        public async Task<ActionResult> Edit(int id)
        {
            Categoria reg = await _gestionCategoria.Buscar(id);
            return View("~/Views/Admin/AdminCategoria/Edit.cshtml", reg);

        }

        [HttpPost]
        public async Task<ActionResult> Edit(Categoria reg)
        {
            TempData["GoodMessage"] = await _gestionCategoria.Actualizar(reg);
            return RedirectToAction("ListaCategoria");
        }

        //DELETE
        public async Task<ActionResult> Delete(int? id = null)
        {
            var categoria = await _gestionCategoria.Buscar(id.Value);

            return View("~/Views/Admin/AdminCategoria/Delete.cshtml", categoria);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            TempData["GoodMessage"] = await _gestionCategoria.Eliminar(id);
            return RedirectToAction("ListaCategoria");
        }

        // DETAILS
        public async Task<ActionResult> Details(int? id = null)
        {
            var Categoria = await _gestionCategoria.Buscar(id.Value);
            return View("~/Views/Admin/AdminCategoria/Details.cshtml", Categoria);
        }
    }
}