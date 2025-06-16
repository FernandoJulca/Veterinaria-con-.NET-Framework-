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
using Dominio.Repositorio;
using static System.Net.Mime.MediaTypeNames;

namespace Infraestructura.Data
{
    public class ProductoDTO : IProducto
    {
        public async Task<IEnumerable<Producto>> Listar()
        {
            List<Producto> productos = new List<Producto>();
            try
            {
                using ( SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_listar_productos_mantenimiento", cnn)) 
                    { 
                    cmd.CommandType =  CommandType.StoredProcedure;
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync()) {
                            while ( await dr.ReadAsync())
                            {
                                byte[] imagen = dr.IsDBNull(2) ? null : (byte[])dr[2];
                                productos.Add(new Producto()
                                {
                                    IdProducto = dr.GetInt32(0),
                                    NombreProducto = dr.GetString(1),
                                    ImagenBase64 = imagen != null ? Convert.ToBase64String(imagen) : null,
                                    IdCategoria = dr.GetInt32(3),
                                    NombreCategoria = dr.GetString(4),
                                    Precio = dr.GetDecimal(5),
                                    Stock = dr.GetInt32(6),
                                    IdEstado = dr.GetInt32(7),
                                    NombreEstado = dr.GetString(8),
                                    flgEliminado = dr.GetBoolean(9)
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
            catch (Exception ex){
                throw new Exception("Error al listar productos", ex);
            }
            return productos;
        }

        public async Task<IEnumerable<Producto>> ListarPorCategoria(int id)
        {
            List<Producto> productos = new List<Producto>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_listar_productos_x_categoria", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idCategoria", id);
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            while (await dr.ReadAsync())
                            {
                                byte[] imagen = dr.IsDBNull(2) ? null : (byte[])dr[2];
                                productos.Add(new Producto()
                                {
                                    IdProducto = dr.GetInt32(0),
                                    NombreProducto = dr.GetString(1),
                                    ImagenBase64 = imagen != null ? Convert.ToBase64String(imagen) : null,
                                    IdCategoria = dr.GetInt32(3),
                                    NombreCategoria = dr.GetString(4),
                                    Precio = dr.GetDecimal(5),
                                    Stock = dr.GetInt32(6),
                                    IdEstado = dr.GetInt32(7),
                                    NombreEstado = dr.GetString(8),
                                    flgEliminado = dr.GetBoolean(9)
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
                throw new Exception("Error al listar productos", ex);
            }
            return productos;
        }

        public async Task<IEnumerable<Producto>> ListarPorNombre(string nombre)
        {
            List<Producto> productos = new List<Producto>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_buscar_producto_nombre", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            while (await dr.ReadAsync())
                            {
                                byte[] imagen = dr.IsDBNull(2) ? null : (byte[])dr[2];
                                productos.Add(new Producto()
                                {
                                    IdProducto = dr.GetInt32(0),
                                    NombreProducto = dr.GetString(1),
                                    ImagenBase64 = imagen != null ? Convert.ToBase64String(imagen) : null,
                                    IdCategoria = dr.GetInt32(3),
                                    NombreCategoria = dr.GetString(4),
                                    Precio = dr.GetDecimal(5),
                                    Stock = dr.GetInt32(6),
                                    IdEstado = dr.GetInt32(7),
                                    NombreEstado = dr.GetString(8),
                                    flgEliminado = dr.GetBoolean(9)
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
                throw new Exception("Error al listar productos", ex);
            }
            return productos;
        }

        public async Task<IEnumerable<Producto>> ListarEntrePrecios(decimal preciomin, decimal preciomax)
        {
            List<Producto> productos = new List<Producto>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_productos_entre_precios", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@preciomin", preciomin);
                        cmd.Parameters.AddWithValue("@preciomax", preciomax);
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            while (await dr.ReadAsync())
                            {
                                byte[] imagen = dr.IsDBNull(2) ? null : (byte[])dr[2];
                                productos.Add(new Producto()
                                {
                                    IdProducto = dr.GetInt32(0),
                                    NombreProducto = dr.GetString(1),
                                    ImagenBase64 = imagen != null ? Convert.ToBase64String(imagen) : null,
                                    IdCategoria = dr.GetInt32(3),
                                    NombreCategoria = dr.GetString(4),
                                    Precio = dr.GetDecimal(5),
                                    Stock = dr.GetInt32(6),
                                    IdEstado = dr.GetInt32(7),
                                    NombreEstado = dr.GetString(8),
                                    flgEliminado = dr.GetBoolean(9)
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
                throw new Exception("Error al listar productos", ex);
            }
            return productos;
        }

        public async Task<string> Agregar(Producto reg)
        {
            string mensaje = "";
            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_insertar_productos", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", reg.NombreProducto);
                        byte[] imagenBytes = null;
                        if (!string.IsNullOrEmpty(reg.ImagenBase64))
                        {
                            imagenBytes = Convert.FromBase64String(reg.ImagenBase64);
                        }
                        if (imagenBytes != null)
                        {
                            cmd.Parameters.Add("@imagen", SqlDbType.VarBinary).Value = imagenBytes;
                        }
                        else
                        {
                            cmd.Parameters.Add("@imagen", SqlDbType.VarBinary).Value = DBNull.Value;
                        }
                        cmd.Parameters.AddWithValue("@categoria", reg.IdCategoria);
                        cmd.Parameters.AddWithValue("@precio", reg.Precio);
                        cmd.Parameters.AddWithValue("@stock", reg.Stock);
                        cmd.Parameters.AddWithValue("@flgEstado", 1);

                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = $"El producto '{reg.NombreProducto}' ha sido registrado correctamente.";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);
            }
            catch (Exception ex) {

                throw new Exception("Error al agregar", ex);
            }
                
            
            return mensaje;
        }

        public async Task<string> Actualizar(Producto reg)
        {
            string mensaje = "";

            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {

                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_actualiza_productos", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@idprod", reg.IdProducto);
                        cmd.Parameters.AddWithValue("@nombre", reg.NombreProducto);
                        byte[] imagenBytes = null;
                        if (!string.IsNullOrEmpty(reg.ImagenBase64))
                        {
                            imagenBytes = Convert.FromBase64String(reg.ImagenBase64);
                        }
                        if (imagenBytes != null)
                        {
                            cmd.Parameters.Add("@imagen", SqlDbType.VarBinary).Value = imagenBytes;
                        }
                        else
                        {
                            cmd.Parameters.Add("@imagen", SqlDbType.VarBinary).Value = DBNull.Value;
                        }
                        cmd.Parameters.AddWithValue("@categoria", reg.IdCategoria);
                        cmd.Parameters.AddWithValue("@precio", reg.Precio);
                        cmd.Parameters.AddWithValue("@stock", reg.Stock);
                        cmd.Parameters.AddWithValue("@flgEstado", reg.IdEstado);

                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = $"El Producto '{reg.NombreProducto}' ha sido actualizado correctamente.";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);
            }
            catch (Exception ex) {
                throw new Exception("Error al actualizar", ex);
            }

          
            return mensaje;
        }


        public async Task<Producto> Buscar(int id)
        {
            var lista = await Listar();
            var producto = lista.FirstOrDefault(x => x.IdProducto == id);

            if (producto == null)
                throw new Exception("Producto no encontrado");

            return producto;
        }


        public async Task<string> Eliminar(int id)
        {
            string mensaje = "";
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                try
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_elimina_productos", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@idprod", id);
                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = "El producto ha sido eliminado correctamente.";
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error en la base de datos", ex);
                }catch (Exception ex)
                {
                    throw new Exception("Error al eliminar", ex);
                }
               
            }
            return mensaje ;
        }
    }
}
