using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidad.Entidad
{
    public class ListadoProductos
    {
        [Display(Name = "Id Producto"), Required] public int IdProducto { get; set; }
        [Display(Name = "Nombre"), Required] public string NombreProducto { get; set; }
        public string ImagenBase64 {get;set;}
        [Display(Name = "Id Categoria"), Required] public int IdCategoria { get; set; }

        [Display(Name = "Categoria")]public string Categoria { get; set; }
        [Display(Name = "Precio"), Required] public decimal Precio { get; set; }
        [Display(Name = "Cantidad Disponible"), Required] public int Stock { get; set; }
        [Display(Name = "Id Estado")] public int IdEstado { get; set; }

        [Display(Name = "Estado")]public string NombreEstado { get; set; }
        public bool flgEliminado { get; set; }
    }
}
