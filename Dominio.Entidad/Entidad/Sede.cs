using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidad.Entidad
{
    public class Sede
    {
        [Display(Name = "Id Sede")]
        public int IdSede { get; set; }
        [Required(ErrorMessage = "El nombre de la sede es obligatorio.")]
        [Display(Name = "Nombre Sede")]
        [StringLength(25, ErrorMessage = "El nombre de la sede no puede exceder los 25 caracteres.")]
        public string Nombre { get; set; }
        [Display(Name = "Imagen")]
        public byte[] Imagen { get; set; }

        public string ImagenBase64 { get; set; }
        [Required(ErrorMessage = "La dirección de la sede es obligatoria.")]
        [StringLength(50, ErrorMessage = "La dirección no puede exceder los 50 caracteres.")]
        [Display(Name = "Ubicacion")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El número de teléfono es obligatorio.")]
        [StringLength(10, ErrorMessage = "El número de teléfono no puede exceder los 10 caracteres.")]
        [RegularExpression(@"^\d{1,10}$", ErrorMessage = "El número de teléfono solo debe contener números.")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
        public bool flgEliminado { get; set; }
    }
}
