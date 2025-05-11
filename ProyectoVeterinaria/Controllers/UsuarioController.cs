using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Aplicacion.Servicios;

namespace ProyectoVeterinaria.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly GestionClientes _gestionClientes = new GestionClientes();
        // GET: Usuario
        public ActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> IniciarSesion(string correo, string contrasenia)
        {
            var cliente = await _gestionClientes.IniciarSesion(correo, contrasenia);
            if (cliente == null)
            {
                ModelState.AddModelError("", "Correo o contraseña incorrectos.");
                return View();
            }

            Session["ClienteId"] = cliente.IdCliente;
            Session["ClienteNombre"] = cliente.NombreCliente;

            return RedirectToAction("Index", "Home");  
        }
        public ActionResult CerrarSesion()
        {
            Session.Clear();          
            Session.Abandon();       
            return RedirectToAction("Index", "Home"); 
        }

        public ActionResult Registro()
        {
            return View();
        }
    }
}