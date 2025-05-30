using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidad.Entidad
{
    public class Servicio
    {
        [Display(Name = "Id Servicio")]
        public int IdServicio { get; set; }
        [Required(ErrorMessage = "El nombre del servicio es obligatorio")]
        [Display(Name = "Nombre ")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
        public string Descripcion { get; set; }
        public bool flgEliminado { get; set; }

    }
}
