using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Aplicacion.Servicios;
using Dominio.Entidad.Entidad;
using Org.BouncyCastle.Ocsp;

namespace ProyectoVeterinaria.Controllers
{
    public class ReservaController : Controller
    {
        private readonly GestionServicios GestionServicios = new GestionServicios();
        private readonly GestionEspecie GestionEspecie = new GestionEspecie();
        private readonly GestionVeterinario GestionVeterinario = new GestionVeterinario();
        private readonly GestionReserva gestionReserva = new GestionReserva();
        private readonly GestionClientes gestionClientes = new GestionClientes();

        public async Task<ActionResult> AgendarCita()
        {
            if (Session["ClienteTipo"] == null || Session["ClienteId"] == null)
            {
                TempData["ErrorMessage"] = "Es necesario iniciar sesión para reservar una cita.";
                return RedirectToAction("IniciarSesion", "Usuario");
            }

            int clienteId = (int)Session["ClienteId"];
            await CargarDatosReserva(clienteId);

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AgendarCita(Reserva r)
        {
            if (ModelState.IsValid)
            {
                r.idCliente = (int)Session["ClienteId"]; 

                var resultado = await gestionReserva.crearReserva(r);
                TempData["GoodMessage"] = resultado;

                return RedirectToAction("Index", "Home");
            }
            int clienteId = (int)Session["ClienteId"];
            await CargarDatosReserva(clienteId);

            return View("AgendarCita", r);
        }
        private async Task CargarDatosReserva(int clienteId)
        {
            var especies = await GestionEspecie.ListarEspecieCliente();
            var servicios = await GestionServicios.Listar();
            var veterinarios = await GestionVeterinario.Listar();
            var cliente = await gestionClientes.ObtenerCliente(clienteId);

            ViewBag.Especies = especies;
            ViewBag.Servicios = servicios;
            ViewBag.Veterinarios = veterinarios.Select(v => new
            {
                v.IdVeterinario,
                NombreCompleto = $"{v.Nombre} {v.Apellido} ({v.Especialidad})"
            }).ToList();

            ViewBag.ClienteNombre = cliente.NombreCliente;
        }

    }
}