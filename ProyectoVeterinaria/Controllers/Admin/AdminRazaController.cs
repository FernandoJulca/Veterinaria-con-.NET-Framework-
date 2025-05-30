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
    public class AdminRazaController : Controller
    {
        private readonly GestionRaza _gestionRaza = new GestionRaza();
        private readonly GestionEspecie gestionEspecie = new GestionEspecie();
        public async Task<ActionResult> ListaRaza(int page = 1, int pageSize = 10)
        {
            var todosLosRaza = await _gestionRaza.ListarRazaCliente();

            var totalItems = todosLosRaza.Count();
            var RazaPaginados = todosLosRaza
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return View("~/Views/Admin/AdminRaza/ListaRaza.cshtml", RazaPaginados);
        }

        //CREATE
        public async Task<ActionResult> Create()
        {
            ViewBag.especies = await gestionEspecie.ListarEspecieGet();
            return View("~/Views/Admin/AdminRaza/Create.cshtml", new Raza());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Raza reg)
        {
            if (ModelState.IsValid)
            {
                TempData["GoodMessage"] = await _gestionRaza.Agregar(reg);
                return RedirectToAction("ListaRaza");
            }
            ViewBag.especies = await gestionEspecie.ListarEspeciePost(reg);
            return View("~/Views/Admin/AdminRaza/Create.cshtml", reg);

        }
        //EDIT
        public async Task<ActionResult> Edit(int id)
        {
            Raza reg = await _gestionRaza.Buscar(id);
            ViewBag.especies = await gestionEspecie.ListarEspeciePost(reg);
            return View("~/Views/Admin/AdminRaza/Edit.cshtml", reg);

        }

        [HttpPost]
        public async Task<ActionResult> Edit(Raza reg)
        {
            TempData["GoodMessage"] = await _gestionRaza.Actualizar(reg);
            ViewBag.especies = await gestionEspecie.ListarEspeciePost(reg);
            return RedirectToAction("ListaRaza");
        }

        //DELETE
        public async Task<ActionResult> Delete(int? id = null)
        {
            var Raza = await _gestionRaza.Buscar(id.Value);

            return View("~/Views/Admin/AdminRaza/Delete.cshtml", Raza);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            TempData["GoodMessage"] = await _gestionRaza.Eliminar(id);
            return RedirectToAction("ListaRaza");
        }

        // DETAILS
        public async Task<ActionResult> Details(int? id = null)
        {
            var Raza = await _gestionRaza.Buscar(id.Value);
            return View("~/Views/Admin/AdminRaza/Details.cshtml", Raza);
        }
    }
}