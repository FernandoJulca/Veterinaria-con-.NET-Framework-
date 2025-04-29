using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidad.Abstraccion;
using Dominio.Entidad.Entidad;

namespace Infraestructura.Data
{
    public class EstadoDTO : IEstado
    {
        public async Task<IEnumerable<Estado>> Listar()
        {
            List<Estado> estados = new List<Estado>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    SqlCommand cmd = new SqlCommand("usp_listar_estado", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            estados.Add(new Estado()
                            {
                                IdEstado = dr.GetInt32(0),
                                Descripcion = dr.GetString(1)
                            });
                        }
                    }
                }


            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);

            }
            catch (Exception ex) {
                throw new Exception("Error al listar estado", ex);
            
            
            }
           return estados;
        }
    }
}
