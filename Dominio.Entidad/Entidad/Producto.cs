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
       public int IdProducto { get; set; }
        public string NombreProducto { get; set; }
        public byte[] Imagen { get; set; }

        public int IdCategoria { get; set; }
         public decimal Precio { get; set; }
        public int Stock { get; set; }
        
        public int IdEstado { get; set; }
        public bool flgEliminado { get; set; }

    }
}
