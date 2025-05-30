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
    public class AdminVeterinarioController : Controller
    {
        private readonly GestionEspecialidad _gestionEspecialidad = new GestionEspecialidad();
        private readonly GestionVeterinario _gestionVeterinario = new GestionVeterinario();

        public async Task<ActionResult> ListaVeterinario(int page = 1, int pageSize = 10)
        {
            var todosLosVeterinario = await _gestionVeterinario.Listar();

            var totalItems = todosLosVeterinario.Count();
            var VeterinarioPaginados = todosLosVeterinario
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return View("~/Views/Admin/AdminVeterinario/ListaVeterinario.cshtml", VeterinarioPaginados);
        }

        //CREATE
        public async Task<ActionResult> Create()
        {
            ViewBag.especialidad = await _gestionEspecialidad.ListarEspecialidadGet();

            return View("~/Views/Admin/AdminVeterinario/Create.cshtml", new Veterinario());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Veterinario reg)
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
                TempData["GoodMessage"] = await _gestionVeterinario.Agregar(reg);
                return RedirectToAction("ListaVeterinario");
            }

            ViewBag.especialidad = await _gestionEspecialidad.ListarEspecialidadPost(reg);
            return View("~/Views/Admin/AdminVeterinario/Create.cshtml", new Veterinario());

        }


        //EDIT
        public async Task<ActionResult> Edit(int id)
        {
            Veterinario reg = await _gestionVeterinario.Buscar(id);
            ViewBag.especialidad = await _gestionEspecialidad.ListarEspecialidadPost(reg);
            return View("~/Views/Admin/AdminVeterinario/Edit.cshtml", reg);

        }

        [HttpPost]
        public async Task<ActionResult> Edit(Veterinario reg)
        {
            TempData["GoodMessage"] = await _gestionVeterinario.Actualizar(reg);
            ViewBag.Especialidad = await _gestionEspecialidad.ListarEspecialidadPost(reg);
            return RedirectToAction("ListaVeterinario");
        }

        //DELETE
        public async Task<ActionResult> Delete(int? id = null)
        {
            var Veterinario = await _gestionVeterinario.Buscar(id.Value);

            return View("~/Views/Admin/AdminVeterinario/Delete.cshtml", Veterinario);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            TempData["GoodMessage"] = await _gestionVeterinario.Eliminar(id);
            return RedirectToAction("ListaVeterinario");
        }

        // DETAILS
        public async Task<ActionResult> Details(int? id = null)
        {
            var Veterinario = await _gestionVeterinario.Buscar(id.Value);
            return View("~/Views/Admin/AdminVeterinario/Details.cshtml", Veterinario);
        }
    }
}