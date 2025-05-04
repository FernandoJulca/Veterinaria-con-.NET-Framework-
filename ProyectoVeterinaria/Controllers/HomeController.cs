using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Aplicacion.Servicios;

namespace ProyectoVeterinaria.Controllers
{
    public class HomeController : Controller
    {
        private readonly GestionVeterinarios gestionVeterinarios = new GestionVeterinarios();
        private readonly GestionServicios gestionServicios = new GestionServicios();
        private readonly GestionSedes gestionSedes = new GestionSedes();

        public async Task<ActionResult> Index()
        {
            ViewBag.veterinarios = await gestionVeterinarios.Listar();
            ViewBag.sedes = await gestionSedes.Listar();
            ViewBag.servicios = await gestionServicios.Listar();
            return View();
        }

        public async Task<ActionResult> Nosotros()
        {
            ViewBag.veterinarios = await gestionVeterinarios.Listar();

            return View();
        }

        public async Task<ActionResult> Sedes()
        {
            ViewBag.sedes = await gestionSedes.Listar();
            return View();
        }
    }
}