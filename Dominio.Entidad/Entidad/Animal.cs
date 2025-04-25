using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Entidad.Entidad
{
    public class Animal
    {
        public int IdAnimal {  get; set; }
        public string NombreAnimal { get; set; }
        public int IdEspecie {  get; set; }
        public string Raza { get; set; }
        public string Sexo { get; set; }
        public int Edad {  get; set; }
        public int FlgEstado { get; set; }
    }
}
