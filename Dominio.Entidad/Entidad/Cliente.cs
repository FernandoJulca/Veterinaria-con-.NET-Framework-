using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidad.Entidad
{
    public class Cliente
    {
        [Display(Name ="Id Cliente"),Required]public int IdCliente { get; set; }
        [Display(Name = "Nombre Cliente"), Required] public string NombreCliente { get; set; }
        [Display(Name = "Apellido Cliente"), Required] public string ApellidoCliente { get; set; }
        [Display(Name = "DNI"), Required] public string Documento { get; set; }
        [Display(Name = "Teléfono"), Required] public string Telefono { get; set; }
        [Display(Name = "Correo"), Required] public string Correo { get; set; }
        [Display(Name = "Dirección"), Required] public string Direccion { get; set; }
        [Display(Name = "Estado"), Required] public int FlgEstado { get; set; }

    }
}
