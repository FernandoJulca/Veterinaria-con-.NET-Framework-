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
    public class AdminEspecieController : Controller
    {
        private readonly GestionEspecie _gestionEspecie = new GestionEspecie();
        public async Task<ActionResult> ListaEspecie(int page = 1, int pageSize = 10)
        {
            var todosLosEspecie = await _gestionEspecie.ListarEspecieCliente();

            var totalItems = todosLosEspecie.Count();
            var EspeciePaginados = todosLosEspecie
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return View("~/Views/Admin/AdminEspecie/ListaEspecie.cshtml", EspeciePaginados);
        }

        //CREATE
        public async Task<ActionResult> Create()
        {
            return View("~/Views/Admin/AdminEspecie/Create.cshtml", new Especie());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Especie reg)
        {
            if (ModelState.IsValid)
            {
                TempData["GoodMessage"] = await _gestionEspecie.Agregar(reg);
                return RedirectToAction("ListaEspecie");

            }
            return View("~/Views/Admin/AdminEspecie/Create.cshtml",reg);

        }
        //EDIT
        public async Task<ActionResult> Edit(int id)
        {
            Especie reg = await _gestionEspecie.Buscar(id);
            return View("~/Views/Admin/AdminEspecie/Edit.cshtml", reg);

        }

        [HttpPost]
        public async Task<ActionResult> Edit(Especie reg)
        {
            TempData["GoodMessage"] = await _gestionEspecie.Actualizar(reg);
            return RedirectToAction("ListaEspecie");
        }

        //DELETE
        public async Task<ActionResult> Delete(int? id = null)
        {
            var Especie = await _gestionEspecie.Buscar(id.Value);

            return View("~/Views/Admin/AdminEspecie/Delete.cshtml", Especie);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            TempData["GoodMessage"] = await _gestionEspecie.Eliminar(id);
            return RedirectToAction("ListaEspecie");
        }

        // DETAILS
        public async Task<ActionResult> Details(int? id = null)
        {
            var Especie = await _gestionEspecie.Buscar(id.Value);
            return View("~/Views/Admin/AdminEspecie/Details.cshtml", Especie);
        }
    }
}