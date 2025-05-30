using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Aplicacion.Servicios;
using Dominio.Entidad.Entidad;

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
                TempData["ErrorMessage"] = "Usuario o contraseña incorrectos.";
                return View();
            }

            Session["ClienteId"] = cliente.IdCliente;
            Session["ClienteNombre"] = cliente.NombreCliente;
            Session["ClienteApellido"] = cliente.ApellidoCliente;
            Session["ClienteTipo"] = cliente.Tipo;

            TempData["GoodMessage"] = $"{cliente.NombreCliente} {cliente.ApellidoCliente} ingresado con éxito";

            if (cliente.Tipo == "A")
            {
                return RedirectToAction("Index", "Admin"); 
            }
            else if (cliente.Tipo == "C")
            {
                return RedirectToAction("Index", "Home"); 
            }
            else
            {
                TempData["ErrorMessage"] = "Tipo de usuario no reconocido.";
                return RedirectToAction("IniciarSesion", "Usuario");
            }
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
        [HttpPost]
        public async Task<ActionResult> Registro(Cliente c)
        {
            string cliente = await _gestionClientes.RegistrarCliente(c);
            if (cliente.Contains("correctamente"))
            {
                TempData["GoodMessage"] = cliente;
                return RedirectToAction("IniciarSesion", "Usuario");
            }
            else
            {
                TempData["ErrorMessage"] = cliente;
                return View();
            }
            
        }
    }
}