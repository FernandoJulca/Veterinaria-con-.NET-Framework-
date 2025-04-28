using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio.Entidad.Abstraccion;
using Dominio.Entidad.Entidad;

namespace Infraestructura.Data
{
    public class ProductoDTO : IProducto
    {
        public IEnumerable<ListadoProductos> GetAll()
        {
            List<ListadoProductos> productos = new List<ListadoProductos>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("usp_listar_productos_mantenimiento", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read()) { 
                        productos.Add(new ListadoProductos()
                        {
                            IdProducto = dr.GetInt32(0),
                            NombreProducto = dr.GetString(1),
                            IdCategoria = dr.GetInt32(2),
                            Categoria = dr.GetString(3),
                            Precio = dr.GetDecimal(4),
                            Stock = dr.GetInt32(5),
                            Estado = dr.GetString(6)
                        });
                    
                    }
                    dr.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return productos;
        }

        public string Agregar(Producto reg)
        {
            string mensaje = "";

            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                try
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("usp_insertar_productos", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    
                    cmd.Parameters.AddWithValue("@nombre", reg.NombreProducto);
                    cmd.Parameters.AddWithValue("@categoria", reg.IdCategoria);
                    cmd.Parameters.AddWithValue("@precio", reg.Precio);
                    cmd.Parameters.AddWithValue("@stock", reg.Stock);
                    cmd.Parameters.AddWithValue("@flgEstado", 1);
                    
                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"se ha registrado el producto {reg.NombreProducto}";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                finally { 
                
                    cnn.Close();
                
                }
            }
            return mensaje;
        }

        public string Actualizar(Producto reg)
        {
            string mensaje = "";

            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                try
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("usp_actualiza_productos", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@idprod", reg.IdProducto);
                    cmd.Parameters.AddWithValue("@nombre", reg.NombreProducto);
                    cmd.Parameters.AddWithValue("@categoria", reg.IdCategoria);
                    cmd.Parameters.AddWithValue("@precio", reg.Precio);
                    cmd.Parameters.AddWithValue("@stock", reg.Stock);
                   

                    int i = cmd.ExecuteNonQuery();
                    mensaje = $"se ha actualizado el producto con el Id{reg.IdProducto}";
                }
                catch (Exception ex)
                {
                    mensaje = ex.Message;
                }
                finally
                {

                    cnn.Close();

                }
            }
            return mensaje;
        }
    }
}
