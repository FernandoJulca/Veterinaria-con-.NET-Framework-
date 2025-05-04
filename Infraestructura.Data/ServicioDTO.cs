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
    public class ServicioDTO : IServicio
    {
        public async Task<IEnumerable<Servicio>> Listar()
        {
            List<Servicio> list = new List<Servicio>();
            try
            {
                using(SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString)) {
                    await cnn.OpenAsync();
                    using(SqlCommand cmd = new SqlCommand("usp_list_servicios", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using(SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(new Servicio()
                                {
                                    IdServicio = reader.GetInt32(0),
                                    Descripcion = reader.GetString(1)
                                });
                            }
                            reader.Close();
                        }
                    }
                }
            }catch(Exception ex)
            {
                throw new Exception("Error al listar productos", ex);
            }
            return list;
        }
    }
}
