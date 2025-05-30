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
    public class AdminServicioController : Controller
    {
        private readonly GestionServicios _gestionServicio = new GestionServicios();
        public async Task<ActionResult> ListaServicio(int page = 1, int pageSize = 10)
        {
            var todosLosServicios = await _gestionServicio.Listar();

            var totalItems = todosLosServicios.Count();
            var ServiciosPaginados = todosLosServicios
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return View("~/Views/Admin/AdminServicio/ListaServicio.cshtml", ServiciosPaginados);
        }

        //CREATE
        public async Task<ActionResult> Create()
        {
            return View("~/Views/Admin/AdminServicio/Create.cshtml", new Servicio());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Servicio reg)
        {
            if (ModelState.IsValid)
            {
                TempData["GoodMessage"] = await _gestionServicio.Agregar(reg);
                return RedirectToAction("ListaServicio");
            }

            return View("~/Views/Admin/AdminServicio/Create.cshtml", reg);
        }
        //EDIT
        public async Task<ActionResult> Edit(int id)
        {
            Servicio reg = await _gestionServicio.Buscar(id);
            return View("~/Views/Admin/AdminServicio/Edit.cshtml", reg);

        }

        [HttpPost]
        public async Task<ActionResult> Edit(Servicio reg)
        {
            TempData["GoodMessage"] = await _gestionServicio.Actualizar(reg);
            return RedirectToAction("ListaServicio");
        }

        //DELETE
        public async Task<ActionResult> Delete(int? id = null)
        {
            var Servicio = await _gestionServicio.Buscar(id.Value);

            return View("~/Views/Admin/AdminServicio/Delete.cshtml", Servicio);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            TempData["GoodMessage"] = await _gestionServicio.Eliminar(id);
            return RedirectToAction("ListaServicio");
        }

        // DETAILS
        public async Task<ActionResult> Details(int? id = null)
        {
            var Servicio = await _gestionServicio.Buscar(id.Value);
            return View("~/Views/Admin/AdminServicio/Details.cshtml", Servicio);
        }
    }
}