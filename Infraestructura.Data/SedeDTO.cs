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
    public class SedeDTO : ISede
    {
        public async Task<IEnumerable<Sede>> Listar()
        {
            List<Sede> list = new List<Sede>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_list_sedes", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(new Sede()
                                {
                                    IdSede = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    Direccion = reader.GetString(2),
                                    Telefono = reader.GetString(3)
                                });
                            }
                            reader.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar productos", ex);
            }
            return list;
        }
    }
}
