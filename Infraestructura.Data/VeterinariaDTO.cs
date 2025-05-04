using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidad.Abstraccion;
using Dominio.Entidad.Entidad;

namespace Infraestructura.Data
{
    public class VeterinariaDTO : IVeterinario
    {
        public async Task<IEnumerable<Veterinario>> Listar()
        {
            List<Veterinario> list = new List<Veterinario>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using(SqlCommand cmd = new SqlCommand("usp_list_veterinarios", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync()){
                            while (await reader.ReadAsync())
                            {
                                list.Add(new Veterinario()
                                {
                                    IdVeterinario = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    Apellido = reader.GetString(2),
                                    Especialidad = reader.GetString(3)
                                });
                            }
                            reader.Close();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar veterinarios", ex);
            }
            return list;
        }
    }
}
