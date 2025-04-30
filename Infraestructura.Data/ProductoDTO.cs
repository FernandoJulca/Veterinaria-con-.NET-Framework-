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
                                productos.Add(new Producto()
                                {
                                    IdProducto = dr.GetInt32(0),
                                    NombreProducto = dr.GetString(1),
                                    Imagen = dr.IsDBNull(2) ? null : (byte[])dr[2],
                                    IdCategoria = dr.GetInt32(3),
                                    Precio = dr.GetDecimal(4),
                                    Stock = dr.GetInt32(5),
                                    IdEstado = dr.GetInt32(6),
                                    flgEliminado = dr.GetBoolean(7)
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
                        cmd.Parameters.AddWithValue("@categoria", reg.IdCategoria);
                        cmd.Parameters.AddWithValue("@precio", reg.Precio);
                        cmd.Parameters.AddWithValue("@stock", reg.Stock);
                        cmd.Parameters.AddWithValue("@flgEstado", 1);

                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = $"se ha registrado el producto {reg.NombreProducto}";
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
                        cmd.Parameters.AddWithValue("@categoria", reg.IdCategoria);
                        cmd.Parameters.AddWithValue("@precio", reg.Precio);
                        cmd.Parameters.AddWithValue("@stock", reg.Stock);


                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = $"se ha actualizado el producto con el Id{reg.IdProducto}";
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
                        mensaje = $"Se ha eliminado el producto seleccionado";
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
