using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidad.Entidad;
using Dominio.Entidad.Abstraccion;

namespace Infraestructura.Data
{
    public class CategoriaDTO :ICategoria
    {
        public async Task<IEnumerable<Categoria>> Listar()
        {
            List<Categoria> categorias = new List<Categoria>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_listar_categoria", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            while (dr.Read())
                            {
                                categorias.Add(new Categoria()
                                {
                                    IdCategoria = dr.GetInt32(0),
                                    NombreCategoria = dr.GetString(1)
                                });

                            }
                        }
                    }


                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar categoria", ex);
            }
            return categorias;

        }
    }
}
