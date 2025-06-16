using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using Aplicacion.Servicios;
using Dominio.Entidad.Abstraccion;
using Dominio.Entidad.Entidad;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Font = iTextSharp.text.Font;

namespace ProyectoVeterinaria.Controllers.Admin
{
    public class AdminProductoController : Controller
    {
        private readonly GestionProductos _gestionProductos = new GestionProductos();
        private readonly GestionEstados _gestionEstados = new GestionEstados();
        private readonly GestionCategoria _gestionCategoria = new GestionCategoria();

        public async Task<ActionResult> ListaProductos(
    string nombre = null,
    int? categoria = null,
    decimal? precioMin = null,
    decimal? precioMax = null,
    int page = 1,
    int pageSize = 10)
        {
            List<ListadoProductos> productos = await _gestionProductos.ObtenerListadoProductos();

            if (!string.IsNullOrEmpty(nombre))
                productos = productos
                    .Where(p => p.NombreProducto != null &&
                                p.NombreProducto.IndexOf(nombre, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

            if (categoria.HasValue)
                productos = productos
                    .Where(p => p.IdCategoria == categoria.Value)
                    .ToList();

            if (precioMin.HasValue)
                productos = productos
                    .Where(p => p.Precio >= precioMin.Value)
                    .ToList();

            if (precioMax.HasValue)
                productos = productos
                    .Where(p => p.Precio <= precioMax.Value)
                    .ToList();

            var totalItems = productos.Count();
            var productosPaginados = productos
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            ViewBag.FiltroNombre = nombre;
            ViewBag.FiltroCategoria = categoria;
            ViewBag.FiltroPrecioMin = precioMin;
            ViewBag.FiltroPrecioMax = precioMax;
            ViewBag.Categorias = await _gestionCategoria.ListarCategoriaCliente();

            return View("~/Views/Admin/AdminProducto/ListaProductos.cshtml", productosPaginados);
        }

        public async Task<ActionResult> GenerarReporte(string nombre = null, int? categoria = null, decimal? precioMin = null, decimal? precioMax = null)
        {
            List<ListadoProductos> productos = await _gestionProductos.ObtenerListadoProductos();

            if (!string.IsNullOrEmpty(nombre))
                productos = productos
                    .Where(p => p.NombreProducto != null &&
                                p.NombreProducto.IndexOf(nombre, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

            if (categoria.HasValue)
                productos = productos.Where(p => p.IdCategoria == categoria.Value).ToList();

            if (precioMin.HasValue)
                productos = productos.Where(p => p.Precio >= precioMin.Value).ToList();

            if (precioMax.HasValue)
                productos = productos.Where(p => p.Precio <= precioMax.Value).ToList();

            string nombreCategoria = "Todas";
            if (categoria.HasValue)
            {
                var categorias = await _gestionCategoria.ListarCategoriaCliente();
                var cat = categorias.FirstOrDefault(c => c.IdCategoria == categoria.Value);
                if (cat != null)
                {
                    nombreCategoria = cat.NombreCategoria;
                }
            }

            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 25, 25, 30, 30);
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                string logoPath = Server.MapPath("~/imagenes/logo-bn.png");
                if (System.IO.File.Exists(logoPath))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                    logo.ScaleToFit(120f, 50f);
                    logo.Alignment = Element.ALIGN_LEFT;
                    doc.Add(logo);
                }

                var titulo = new Paragraph("Reporte de Productos")
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingBefore = 5f,
                    SpacingAfter = 5f,
                    Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 26)
                };
                doc.Add(titulo);

                string subtituloTexto = "";

                bool sinFiltros = string.IsNullOrEmpty(nombre) && !categoria.HasValue && !precioMin.HasValue && !precioMax.HasValue;

                if (sinFiltros)
                {
                    subtituloTexto = "Todos los productos disponibles";
                }
                else
                {
                    List<string> partes = new List<string>();

                    if (!string.IsNullOrEmpty(nombre))
                        partes.Add($"con nombre que contiene \"{nombre}\"");

                    if (categoria.HasValue)
                        partes.Add($"de la categoría \"{nombreCategoria}\"");

                    if (precioMin.HasValue || precioMax.HasValue)
                    {
                        string rango = "con precios";
                        if (precioMin.HasValue)
                            rango += $" desde {precioMin.Value:N2}";
                        if (precioMax.HasValue)
                            rango += $" hasta {precioMax.Value:N2}";
                        partes.Add(rango);
                    }

                    subtituloTexto = "Productos " + string.Join(" ", partes);
                }


                var subtitulo = new Paragraph(subtituloTexto,
                    FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 12, BaseColor.DARK_GRAY))
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 10f
                };
                doc.Add(subtitulo);

                Paragraph fecha = new Paragraph($"Fecha de generación: {DateTime.Now:dd/MM/yyyy HH:mm}",
                    FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 10))
                {
                    Alignment = Element.ALIGN_RIGHT,
                    SpacingBefore = 10f,
                    SpacingAfter = 20f
                };
                doc.Add(fecha);

                PdfPTable tabla = new PdfPTable(5);
                tabla.WidthPercentage = 100;
                tabla.SetWidths(new float[] { 10f, 40f, 20f, 15f, 15f });

                BaseColor headerColor = new BaseColor(147, 112, 219);
                Font headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE);

                string[] headers = { "Id", "Nombre", "Categoría", "Precio", "Stock" };
                foreach (var header in headers)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(header, headerFont));
                    cell.BackgroundColor = headerColor;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Padding = 5;
                    tabla.AddCell(cell);
                }

                Font rowFont = FontFactory.GetFont(FontFactory.HELVETICA, 11, BaseColor.BLACK);
                foreach (var p in productos)
                {
                    tabla.AddCell(new PdfPCell(new Phrase(p.IdProducto.ToString(), rowFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                    tabla.AddCell(new PdfPCell(new Phrase(p.NombreProducto, rowFont)));
                    tabla.AddCell(new PdfPCell(new Phrase(p.Categoria, rowFont)));
                    tabla.AddCell(new PdfPCell(new Phrase(p.Precio.ToString("N2"), rowFont)) { HorizontalAlignment = Element.ALIGN_RIGHT });
                    tabla.AddCell(new PdfPCell(new Phrase(p.Stock.ToString(), rowFont)) { HorizontalAlignment = Element.ALIGN_CENTER });
                }

                doc.Add(tabla);

                doc.Close();

                byte[] byteInfo = ms.ToArray();
                return File(byteInfo, "application/pdf", "ReporteProductos.pdf");
            }
        }

        //CREATE
        public async Task<ActionResult> Create()
        {
            ViewBag.categorias = await _gestionCategoria.ListarCategoria();

            return View("~/Views/Admin/AdminProducto/Create.cshtml",new Producto());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Producto reg)
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
                TempData["GoodMessage"] = await _gestionProductos.AgregarProducto(reg);
                return RedirectToAction("ListaProductos");
            }

            ViewBag.categorias = await _gestionCategoria.ListarCategoriaPost(reg);
            return View("~/Views/Admin/AdminProducto/Create.cshtml",reg);
        }


        //EDIT
        public async Task<ActionResult> Edit(int id)
        {
            Producto reg = await _gestionProductos.BuscarProducto(id);
            ViewBag.categorias = await _gestionCategoria.ListarCategoriaPost(reg);
            ViewBag.estados = await _gestionEstados.Listar(reg);
            return View("~/Views/Admin/AdminProducto/Edit.cshtml", reg);

        }

        [HttpPost]
        public async Task<ActionResult> Edit(Producto reg)
        {
            TempData["GoodMessage"] = await _gestionProductos.ActualizaProducto(reg);
            ViewBag.categorias = await _gestionCategoria.ListarCategoriaPost(reg);
            ViewBag.estados = await _gestionEstados.Listar(reg);
            return RedirectToAction("ListaProductos");
        }

        //DELETE
        public async Task<ActionResult> Delete(int? id = null)
        {
            var producto = await _gestionProductos.BuscarProducto(id.Value);

            return View("~/Views/Admin/AdminProducto/Delete.cshtml", producto);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            TempData["GoodMessage"] = await _gestionProductos.EliminarProducto(id);
            return RedirectToAction("ListaProductos");
        }

        // DETAILS
        public async Task<ActionResult> Details(int? id = null)
        {
            var producto = await _gestionProductos.BuscarProducto(id.Value);
            return View("~/Views/Admin/AdminProducto/Details.cshtml", producto);
        }

    }
}