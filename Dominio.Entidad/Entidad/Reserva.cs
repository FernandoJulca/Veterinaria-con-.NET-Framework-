using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidad.Entidad
{
    public class Reserva
    {
		public int idAtencion { get; set; }
        public DateTime fecha { get; set; }
        public string motivo { get; set; }
        public int idCliente { get; set; }
        public string nombreCliente { get; set; }
        public string apellidoCliente { get; set; }
        public int idAnimal { get; set; }
        public string nombreAnimal { get; set; }
        public string nombreMascota { get; set; }
        public int idServicio { get; set; }
        public string nombreServicio { get; set; }
        public int idVeterinario{ get; set; }
        public string nombreVeterinario { get; set; }
    }
}
