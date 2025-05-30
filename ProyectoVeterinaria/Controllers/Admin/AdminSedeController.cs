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
    public class AdminSedeController : Controller
    {
        private readonly GestionSedes _gestionSede = new GestionSedes();
        public async Task<ActionResult> ListaSede(int page = 1, int pageSize = 10)
        {
            var todosLosSede = await _gestionSede.Listar();

            var totalItems = todosLosSede.Count();
            var SedePaginados = todosLosSede
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return View("~/Views/Admin/AdminSede/ListaSede.cshtml", SedePaginados);
        }

        //CREATE
        public async Task<ActionResult> Create()
        {
            return View("~/Views/Admin/AdminSede/Create.cshtml", new Sede());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Sede reg)
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
                TempData["GoodMessage"] = await _gestionSede.Agregar(reg);
                return RedirectToAction("ListaSede");
            }

            return View("~/Views/Admin/AdminSede/Create.cshtml", reg);

        }
        //EDIT
        public async Task<ActionResult> Edit(int id)
        {
            Sede reg = await _gestionSede.Buscar(id);
            return View("~/Views/Admin/AdminSede/Edit.cshtml", reg);

        }

        [HttpPost]
        public async Task<ActionResult> Edit(Sede reg)
        {
            TempData["GoodMessage"] = await _gestionSede.Actualizar(reg);
            return RedirectToAction("ListaSede");
        }

        //DELETE
        public async Task<ActionResult> Delete(int? id = null)
        {
            var Sede = await _gestionSede.Buscar(id.Value);

            return View("~/Views/Admin/AdminSede/Delete.cshtml", Sede);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            TempData["GoodMessage"] = await _gestionSede.Eliminar(id);
            return RedirectToAction("ListaSede");
        }

        // DETAILS
        public async Task<ActionResult> Details(int? id = null)
        {
            var Sede = await _gestionSede.Buscar(id.Value);
            return View("~/Views/Admin/AdminSede/Details.cshtml", Sede);
        }
    }
}