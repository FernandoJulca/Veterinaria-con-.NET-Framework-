using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidad.Entidad
{
    public class Especie
    {
        [Display(Name = "Id Especie")]
        public int IdEspecie { get; set; }
        [Required(ErrorMessage = "El nombre de la especie es obligatorio")]
        [Display(Name = "Nombre")]
        [StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres")]
        public string NombreEspecie { get; set; }
        public bool flgEliminado { get; set; }

    }
}
