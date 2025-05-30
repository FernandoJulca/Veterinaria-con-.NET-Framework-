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
    public class SedeDTO : ISede
    {
        public async Task<string> Actualizar(Sede reg)
        {
            string mensaje = "";
            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_update_sedes", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idSede", reg.IdSede);
                        cmd.Parameters.AddWithValue("@Nombre", reg.Nombre);
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
                        cmd.Parameters.AddWithValue("@Direccion", reg.Direccion);
                        cmd.Parameters.AddWithValue("@Telefono", reg.Telefono);
                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = $"La sede '{reg.Nombre}' ha sido actualizada correctamente.";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);
            }
            return mensaje;
        }

        public async Task<string> Agregar(Sede reg)
        {
            string mensaje = "";
            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_create_sedes", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", reg.Nombre);
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
                        cmd.Parameters.AddWithValue("@Direccion", reg.Direccion);
                        cmd.Parameters.AddWithValue("@Telefono", reg.Telefono);
                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = $"La sede '{reg.Nombre}' ha sido registrada correctamente.";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);
            }
            return mensaje;
        }

        public async Task<Sede> Buscar(int id)
        {
            var lista = await Listar();
            var Sede = lista.FirstOrDefault(x => x.IdSede == id);

            if (Sede == null)
                throw new Exception("Sede no encontrado");

            return Sede;
        }

        public async Task<string> Eliminar(int id)
        {
            string mensaje = "";
            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_delete_sedes", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idSede", id);
                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = "La sede ha sido eliminada correctamente.";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);
            }
            return mensaje;
        }

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
                                byte[] imagen = reader.IsDBNull(2) ? null : (byte[])reader[2];
                                list.Add(new Sede()
                                {
                                    IdSede = reader.GetInt32(0),
                                    Nombre = reader.GetString(1),
                                    ImagenBase64 = imagen != null ? Convert.ToBase64String(imagen) : null,
                                    Direccion = reader.GetString(3),
                                    Telefono = reader.GetString(4)
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
