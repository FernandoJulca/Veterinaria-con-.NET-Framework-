using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidad.Entidad
{
    public class Carro
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }

        public byte[] Imagen { get; set; }

        public string ImagenBase64 { get; set; }

        public int Cantidad { get; set; }
        public decimal Precio { get; set; }

        public decimal SubTotal { get { return Cantidad * Precio; } }
    }
}
