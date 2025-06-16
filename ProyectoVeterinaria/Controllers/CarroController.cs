using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Aplicacion.Servicios;
using Dominio.Entidad.Abstraccion;
using Dominio.Entidad.Entidad;
using Newtonsoft.Json;

namespace ProyectoVeterinaria.Controllers
{
    public class CarroController : Controller
    {
        // GET: Carro
        private readonly GestionProductos _gestionProductos = new GestionProductos();
        private readonly GestionVenta _gestionVenta = new GestionVenta();

        public async Task<ActionResult> AgregarAlCarrito(int id)
        {
            var producto = await _gestionProductos.BuscarProducto(id);

            if (producto == null)
                return RedirectToAction("Productos", "Producto");

            // Obtener el carrito de la sesión
            var carrito = Session["carrito"] as List<Carro> ?? new List<Carro>();

            // Buscar si el producto ya existe en el carrito
            var itemExistente = carrito.FirstOrDefault(c => c.IdProducto == producto.IdProducto);

            if (itemExistente != null)
            {
                // Si ya está, aumenta la cantidad
                itemExistente.Cantidad++;
            }
            else
            {
                // Si no está, lo agregamos como nuevo ítem
                carrito.Add(new Carro
                {
                    IdProducto = producto.IdProducto,
                    Nombre = producto.NombreProducto,
                    Imagen = producto.Imagen,
                    Precio = producto.Precio,
                    Cantidad = 1
                });
            }

            // Guardar el carrito actualizado en sesión
            Session["carrito"] = carrito;

            return RedirectToAction("Productos", "Producto");
        }


        public JsonResult ObtenerCarrito()
        {
            var carrito = Session["carrito"] as List<Carro> ?? new List<Carro>();

            var resultado = carrito.Select(c => new
            {
                c.IdProducto,
                c.Nombre,
                c.Precio,
                c.Cantidad,
                SubTotal = c.Cantidad * c.Precio,
                ImagenBase64 = c.Imagen != null
                    ? $"data:image/png;base64,{Convert.ToBase64String(c.Imagen)}"
                    : null
            });

            return Json(resultado, JsonRequestBehavior.AllowGet);
        }



        public ActionResult ProcesarCompra()
        {

            var carrito = Session["carrito"] as List<Carro> ?? new List<Carro>();


            return View(carrito);
        }


        public ActionResult ContarItemsCarrito()
        {
            var carrito = Session["carrito"] as List<Carro> ?? new List<Carro>();
            int totalUnidades = carrito.Sum(c => c.Cantidad);
            return Json(totalUnidades, JsonRequestBehavior.AllowGet);
        }


        public async Task<ActionResult> ConfirmarCompra()
        {
            var carrito = Session["carrito"] as List<Carro>;
            int? idCliente = Session["ClienteId"] as int?;
            var tipoCliente = Session["ClienteTipo"];

            if (tipoCliente == null)
            {
                TempData["mensaje"] = "Es necesario iniciar sesión para realizar la compra.";
                return RedirectToAction("IniciarSesion", "Usuario"); // Redirige al login
            }

            if (carrito == null || carrito.Count == 0)
            {
                TempData["mensaje"] = "No hay productos en el carrito.";
                return RedirectToAction("Productos", "Producto");
            }

            string resultado = await _gestionVenta.AgregarVenta(idCliente.Value, carrito);

            if (resultado.Contains("éxito"))
            {
                Session["carrito"] = null;
            }

            TempData["mensaje"] = resultado;
            return RedirectToAction("Productos", "Producto");
        }



    }
}