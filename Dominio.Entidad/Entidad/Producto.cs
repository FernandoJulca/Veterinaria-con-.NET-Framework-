using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidad.Entidad
{
    using System.ComponentModel.DataAnnotations;

    public class Producto
    {
        public int IdProducto { get; set; }

        [Required(ErrorMessage = "El nombre del producto es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
        public string NombreProducto { get; set; }
        public byte[] Imagen { get; set; }

        public string ImagenBase64 { get; set; }

        [Required(ErrorMessage = "Debe seleccionar una categoría")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una categoría válida")]
        public int IdCategoria { get; set; }

        public string NombreCategoria { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "El precio debe ser un número positivo")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "El stock es obligatorio")]
        [Range(1, int.MaxValue, ErrorMessage = "El stock no puede ser negativo")]
        public int Stock { get; set; }

        public int IdEstado { get; set; }

        public string NombreEstado { get; set; }

        public bool flgEliminado { get; set; }
    }

}
