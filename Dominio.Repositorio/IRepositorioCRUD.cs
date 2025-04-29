using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Repositorio
{
    public interface IRepositorioCRUD<T> where T : class
    {
        Task<string> Agregar(T reg);
        Task<string> Actualizar(T reg);

        Task<T> Buscar(int id);

        Task<string> Eliminar(int id);
    }
}
