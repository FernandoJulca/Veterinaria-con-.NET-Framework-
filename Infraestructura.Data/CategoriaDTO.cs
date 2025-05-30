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
    public class CategoriaDTO : ICategoria
    {
        public async Task<string> Actualizar(Categoria reg)
        {
            string mensaje = "";
            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_update_categoria", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idCat", reg.IdCategoria);
                        cmd.Parameters.AddWithValue("@Descripcion", reg.NombreCategoria);
                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = $"La categoría '{reg.NombreCategoria}' ha sido actualizada correctamente.";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);
            }
            return mensaje;
        }

        public async Task<string> Agregar(Categoria reg)
        {
            string mensaje = "";
            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_create_categoria", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Descripcion", reg.NombreCategoria);
                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = $"La categoría '{reg.NombreCategoria}' ha sido registrada correctamente.";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);
            }
            return mensaje;
        }

        public async Task<Categoria> Buscar(int id)
        {
            var lista = await Listar();
            var categoria = lista.FirstOrDefault(x => x.IdCategoria == id);

            if (categoria == null)
                throw new Exception("Categoria no encontrado");

            return categoria;
        }

        public async Task<string> Eliminar(int id)
        {
            string mensaje = "";
            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_delete_categoria", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idCat", id);
                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = "La categoría ha sido eliminada correctamente.";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);
            }
            return mensaje;
        }

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
