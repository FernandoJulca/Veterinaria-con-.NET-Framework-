using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidad.Entidad
{
    public class Veterinario
    {
        [Display(Name = "Id Veterinario")]
        public int IdVeterinario { get; set; }
        [Required(ErrorMessage = "El nombre del veterinario es obligatorio")]
        [StringLength(25, ErrorMessage = "El nombre no puede exceder los 25 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "El a´pellido del veterinario es obligatorio")]
        [StringLength(30, ErrorMessage = "El apellido no puede exceder los 30 caracteres")]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }
        [Display(Name = "Imagen")]
        public byte[] Imagen { get; set; }
        public string ImagenBase64 { get; set; }
        [Required(ErrorMessage = "Debe seleccionar una especialidad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una especialidad válida")]
        public int IdEspecialidad { get; set; }
        [Display(Name = "Especialidad")]
        public string Especialidad { get; set; }

        public bool flgEliminado { get; set; }
    }
}
