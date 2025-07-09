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
    public class EspecialidadDTO : IEspecialidad
    {
        public async Task<string> Actualizar(Especialidad reg)
        {
            string mensaje = "";
            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_update_Especialidad", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdEspecialidad", reg.IdEspecialidad);
                        cmd.Parameters.AddWithValue("@Nombre", reg.NombreEspecialidad);
                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = $"La especialidad '{reg.NombreEspecialidad}' ha sido actualizada correctamente.";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);
            }
            return mensaje;
        }

        public async Task<string> Agregar(Especialidad reg)
        {
            string mensaje = "";
            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_create_especialidad", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", reg.NombreEspecialidad);
                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = $"La especialidad '{reg.NombreEspecialidad}' ha sido registrada correctamente.";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);
            }
            return mensaje;
        }

        public async Task<Especialidad> Buscar(int id)
        {
            var lista = await Listar();
            var Especialidad = lista.FirstOrDefault(x => x.IdEspecialidad == id);

            if (Especialidad == null)
                throw new Exception("Especialidad no encontrado");

            return Especialidad;
        }

        public async Task<string> Eliminar(int id)
        {
            string mensaje = "";
            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_delete_especialidad", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdEspecialidad", id);
                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = "La especialidad ha sido eliminada correctamente.";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);
            }
            return mensaje;
        }

        public async Task<IEnumerable<Especialidad>> Listar()
        {
            List<Especialidad> list = new List<Especialidad>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_listar_especialidad", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(new Especialidad()
                                {
                                    IdEspecialidad = reader.GetInt32(0),
                                    NombreEspecialidad = reader.GetString(1)
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
