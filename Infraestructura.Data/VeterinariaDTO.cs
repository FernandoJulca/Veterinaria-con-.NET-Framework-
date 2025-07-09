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
    public class VeterinariaDTO : IVeterinario
    {
        public async Task<string> Agregar(Veterinario reg)
        {
            string mensaje = "";
            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_create_veterinarios", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", reg.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", reg.Apellido);
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
                        cmd.Parameters.AddWithValue("@IdEspecialidad", reg.IdEspecialidad);

                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = $"El veterinario '{reg.Nombre}' ha sido registrada correctamente.";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);
            }
            catch (Exception ex)
            {

                throw new Exception("Error al agregar", ex);
            }


            return mensaje;
        }

        public async Task<string> Actualizar(Veterinario reg)
        {
            string mensaje = "";

            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {

                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_update_veterinarios", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdVeterinario", reg.IdVeterinario);
                        cmd.Parameters.AddWithValue("@Nombre", reg.Nombre);
                        cmd.Parameters.AddWithValue("@Apellido", reg.Apellido);
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
                        cmd.Parameters.AddWithValue("@IdEspecialidad", reg.IdEspecialidad);

                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = $"El veterinario '{reg.Nombre}' ha sido actualizado correctamente.";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar", ex);
            }


            return mensaje;
        }


        public async Task<Veterinario> Buscar(int id)
        {
            var lista = await Listar();
            var Veterinario = lista.FirstOrDefault(x => x.IdVeterinario == id);

            if (Veterinario == null)
                throw new Exception("Veterinario no encontrado");

            return Veterinario;
        }


        public async Task<string> Eliminar(int id)
        {
            string mensaje = "";
            using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                try
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_delete_veterinarios", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@IdVeterinario", id);
                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = "El veterinario ha sido eliminada correctamente.";
                    }
                }
                catch (SqlException ex)
                {
                    throw new Exception("Error en la base de datos", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error al eliminar", ex);
                }

            }
            return mensaje;
        }


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
                                byte[] imagen = reader.IsDBNull(3) ? null : (byte[])reader[3];
                                list.Add(new Veterinario()
                                {
                                    IdVeterinario = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    Apellido = reader.GetString(2),
                                    ImagenBase64 = imagen != null ? Convert.ToBase64String(imagen) : null,
                                    IdEspecialidad = reader.GetInt32(4),
                                    Especialidad = reader.GetString(5)
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
