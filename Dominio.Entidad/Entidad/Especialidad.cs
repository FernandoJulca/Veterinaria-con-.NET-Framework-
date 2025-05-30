using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidad.Entidad
{
    public class Especialidad
    {
        [Display(Name = "Id Especialidad")]
        public int IdEspecialidad { get; set; }
        [Required(ErrorMessage = "El nombre de la especialidad es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
        [Display(Name = "Nombre")]
        public string NombreEspecialidad { get; set; }
        public bool flgEliminado { get; set; }
    }
}
