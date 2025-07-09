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
    public class AdminEspecialidadController : Controller
    {
        private readonly GestionEspecialidad _gestionEspecialidad = new GestionEspecialidad();
        public async Task<ActionResult> ListaEspecialidad(int page = 1, int pageSize = 10)
        {
            var todosLosEspecialidad = await _gestionEspecialidad.ListarEspecialidadCliente();

            var totalItems = todosLosEspecialidad.Count();
            var EspecialidadPaginados = todosLosEspecialidad
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return View("~/Views/Admin/AdminEspecialidad/ListaEspecialidad.cshtml", EspecialidadPaginados);
        }

        //CREATE
        public async Task<ActionResult> Create()
        {
            return View("~/Views/Admin/AdminEspecialidad/Create.cshtml", new Especialidad());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Especialidad reg)
        {
            if (ModelState.IsValid)
            {
                TempData["GoodMessage"] = await _gestionEspecialidad.Agregar(reg);
                return RedirectToAction("ListaEspecialidad");
            }

            return View("~/Views/Admin/AdminEspecialidad/Create.cshtml", reg);

        }
        //EDIT
        public async Task<ActionResult> Edit(int id)
        {
            Especialidad reg = await _gestionEspecialidad.Buscar(id);
            return View("~/Views/Admin/AdminEspecialidad/Edit.cshtml", reg);

        }

        [HttpPost]
        public async Task<ActionResult> Edit(Especialidad reg)
        {
            TempData["GoodMessage"] = await _gestionEspecialidad.Actualizar(reg);
            return RedirectToAction("ListaEspecialidad");
        }

        //DELETE
        public async Task<ActionResult> Delete(int? id = null)
        {
            var Especialidad = await _gestionEspecialidad.Buscar(id.Value);

            return View("~/Views/Admin/AdminEspecialidad/Delete.cshtml", Especialidad);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            TempData["GoodMessage"] = await _gestionEspecialidad.Eliminar(id);
            return RedirectToAction("ListaEspecialidad");
        }

        // DETAILS
        public async Task<ActionResult> Details(int? id = null)
        {
            var Especialidad = await _gestionEspecialidad.Buscar(id.Value);
            return View("~/Views/Admin/AdminEspecialidad/Details.cshtml", Especialidad);
        }
    }
}