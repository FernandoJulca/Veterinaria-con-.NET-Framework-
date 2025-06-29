using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Aplicacion.Servicios;
using Dominio.Entidad.Abstraccion;
using Dominio.Entidad.Entidad;
using iTextSharp.text.pdf;
using iTextSharp.text;

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

        public async Task<ActionResult> Perfil()
        {
            int clienteId = (int)Session["ClienteId"];
            var historialCompra = await _gestionClientes.HistorialCompras(clienteId);
            var historialCita = await _gestionClientes.Historial_Citas(clienteId);
            var cliente = await _gestionClientes.ObtenerCliente(clienteId);

            if (cliente == null)
            {
                TempData["ErrorMessage"] = "No se encontró el perfil del usuario.";
                return RedirectToAction("IniciarSesion", "Usuario");
            }

            ViewBag.Cliente = cliente;
            ViewBag.HistorialCompras = historialCompra;
            ViewBag.HistorialCitas = historialCita;
            return View();
        }

        public async Task<ActionResult> DescargarCompraPDF(int id)
        {
            int clienteId = (int)Session["ClienteId"];
            var compras = await _gestionClientes.HistorialCompras(clienteId);
            var venta = compras.FirstOrDefault(v => v.IdVenta == id);

            if (venta == null)
            {
                return new HttpNotFoundResult("Compra no encontrada.");
            }

            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 40f, 40f, 60f, 40f);
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                string imagePath = Server.MapPath("~/imagenes/logo-bn.png"); 
                if (System.IO.File.Exists(imagePath))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagePath);
                    logo.ScaleToFit(120f, 50f);
                    logo.Alignment = Element.ALIGN_LEFT;
                    doc.Add(logo);
                }

                var tituloFuente = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.BLUE);
                var parrafoTitulo = new Paragraph($"🧾 Detalle de Compra", tituloFuente)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                doc.Add(parrafoTitulo);

                var infoFuente = FontFactory.GetFont(FontFactory.HELVETICA, 12);
                doc.Add(new Paragraph($"📅 Fecha: {venta.Fecha.ToString("dd/MM/yyyy")}", infoFuente));
                doc.Add(new Paragraph($"💵 Total: S/. {venta.Total:0.00}", infoFuente));
                doc.Add(new Paragraph(" ", infoFuente));

                PdfPTable table = new PdfPTable(4);
                table.WidthPercentage = 100;
                table.SpacingBefore = 20f;
                table.SpacingAfter = 20f;
                table.SetWidths(new float[] { 3f, 2f, 1f, 2f });

                var headerFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, BaseColor.WHITE);
                var cellBackground = new BaseColor(0, 102, 204); // Azul oscuro

                string[] headers = { "Producto", "Precio Unitario", "Cantidad", "Subtotal" };
                foreach (var header in headers)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(header, headerFont));
                    cell.BackgroundColor = cellBackground;
                    cell.HorizontalAlignment = Element.ALIGN_CENTER;
                    cell.Padding = 5f;
                    table.AddCell(cell);
                }

                var cellFont = FontFactory.GetFont(FontFactory.HELVETICA, 11);
                foreach (var detalle in venta.Detalles)
                {
                    table.AddCell(new PdfPCell(new Phrase(detalle.nombreProducto, cellFont)));
                    table.AddCell(new PdfPCell(new Phrase($"S/. {detalle.precio:0.00}", cellFont)));
                    table.AddCell(new PdfPCell(new Phrase(detalle.cantidad.ToString(), cellFont)));
                    table.AddCell(new PdfPCell(new Phrase($"S/. {(detalle.precio * detalle.cantidad):0.00}", cellFont)));
                }

                doc.Add(table);

                var nota = new Paragraph("Gracias por su compra 🐾", FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 12, BaseColor.GRAY))
                {
                    Alignment = Element.ALIGN_CENTER
                };
                doc.Add(nota);

                doc.Close();

                return File(ms.ToArray(), "application/pdf", $"Compra_{id}.pdf");
            }
        }


        public async Task<ActionResult> DescargarCitaPDF(int id)
        {
            int clienteId = (int)Session["ClienteId"];
            var citas = await _gestionClientes.Historial_Citas(clienteId);
            var cita = citas.FirstOrDefault(c => c.idAtencion == id);

            if (cita == null)
            {
                return new HttpNotFoundResult("Cita no encontrada.");
            }

            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 40f, 40f, 60f, 40f);
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                string imagePath = Server.MapPath("~/imagenes/logo-bn.png");
                if (System.IO.File.Exists(imagePath))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagePath);
                    logo.ScaleToFit(120f, 50f);
                    logo.Alignment = Element.ALIGN_LEFT;
                    doc.Add(logo);
                }

                var tituloFuente = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18, BaseColor.DARK_GRAY);
                var titulo = new Paragraph("📋 Detalle de Cita", tituloFuente)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 20f
                };
                doc.Add(titulo);

                var labelFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12);
                var valueFont = FontFactory.GetFont(FontFactory.HELVETICA, 12);
                var espacio = new Paragraph(" ") { SpacingAfter = 5f };

                void AddLabeledParagraph(string label, string value)
                {
                    var p = new Paragraph();
                    p.Add(new Chunk(label + ": ", labelFont));
                    p.Add(new Chunk(value, valueFont));
                    p.SpacingAfter = 8f;
                    doc.Add(p);
                }

                AddLabeledParagraph("📅 Fecha", cita.fecha.ToString("dd/MM/yyyy"));
                AddLabeledParagraph("📝 Motivo", cita.motivo);
                AddLabeledParagraph("🐾 Tipo de Animal", cita.nombreAnimal);
                AddLabeledParagraph("🐶 Nombre de la Mascota", cita.nombreMascota);
                AddLabeledParagraph("💼 Servicio", cita.nombreServicio);
                AddLabeledParagraph("👨‍⚕️ Veterinario", cita.nombreVeterinario);

                doc.Add(espacio);

                var notaFinal = new Paragraph("Gracias por confiar en nosotros para el cuidado de tu mascota 🐕",
                    FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 12, BaseColor.GRAY))
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingBefore = 20f
                };
                doc.Add(notaFinal);

                doc.Close();
                return File(ms.ToArray(), "application/pdf", $"Cita_{id}.pdf");
            }
        }



    }
}