using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Infraestructura.Data;
namespace ProyectoVeterinaria.Controllers
{
    public class ProductoController : Controller
    {

        ProductoDTO _producto = new ProductoDTO();
        // GET: Producto
        public ActionResult ListaProductos()
        {
            return View(_producto.GetAll());
        }
    }
}