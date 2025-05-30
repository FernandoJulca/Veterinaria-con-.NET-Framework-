using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidad.Entidad
{
    public class Raza
    {
        [Display(Name = "Id Raza")]
        public int IdRaza{ get; set; }
        [Required(ErrorMessage = "El nombre de la raza es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
        [Display(Name = "Nombre")]
        public string NombreRaza{ get; set; }
        [Required(ErrorMessage = "Debe seleccionar una especie")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una especie válida")]
        public int IdEspecie { get; set; }
        [Display(Name = "Especie")]
        public string NombreEspecie { get; set; }
        public bool flgEliminado { get; set; }

    }
}
