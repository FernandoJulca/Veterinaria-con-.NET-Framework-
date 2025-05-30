using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Aplicacion.Servicios;

namespace ProyectoVeterinaria.Controllers
{
    public class AdminController : Controller
    {
        private readonly GestionProductos _gestionProductos = new GestionProductos();
        private readonly GestionVenta gestionVenta = new GestionVenta();
        private readonly GestionClientes _gestionClientes = new GestionClientes();

        public async Task<ActionResult> Index()
        {
            var todosLosProductos = await _gestionProductos.ObtenerListadoProductos();
            var todosLosClientes = await _gestionClientes.Listar();
            var totalProductos = todosLosProductos.Count();
            var totalClientes = todosLosClientes.Count();
            var ventas = await gestionVenta.ListarPorMes();
            ViewBag.ventasMes = ventas.Select(v => v.Mes).ToArray();
            ViewBag.ventasTotal = ventas.Select(v => v.TotalMensual).ToArray();
            ViewBag.productos = totalProductos;
            ViewBag.clientes = totalClientes;
            return View();
        }

        public async Task<ActionResult> ListaCliente(int page = 1, int pageSize = 10)
        {
            var todosLosClientes = await _gestionClientes.Listar();

            var totalItems = todosLosClientes.Count();
            var ClientesPaginados = todosLosClientes
                                        .Skip((page - 1) * pageSize)
                                        .Take(pageSize)
                                        .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            return View(ClientesPaginados);
        }
    }
}