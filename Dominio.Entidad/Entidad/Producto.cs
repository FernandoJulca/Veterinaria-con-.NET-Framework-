using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Entidad.Entidad
{
    public  class Producto
    {
        [Display(Name = "Id Producto"),Required]public int IdProducto { get; set; }
        [Display(Name = "Nombre"), Required] public string NombreProducto { get; set; }
        [Display(Name = "Categoria"), Required] public int IdCategoria { get; set; }
        [Display(Name = "Precio"), Required] public decimal Precio { get; set; }
        [Display(Name = "Cantidad Disponible"), Required] public int Stock { get; set; }
        [Display(Name = "Estado")]public int FlgEstado { get; set; }

    }
}
