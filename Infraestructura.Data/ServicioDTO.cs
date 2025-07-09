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
    public class ServicioDTO : IServicio
    {
        public async Task<string> Actualizar(Servicio reg)
        {
            string mensaje = "";
            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_update_servicios", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idServicio", reg.IdServicio);
                        cmd.Parameters.AddWithValue("@Descripcion", reg.Descripcion);
                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = $"El servicio'{reg.Descripcion}' ha sido actualizada correctamente.";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);
            }
            return mensaje;
        }

        public async Task<string> Agregar(Servicio reg)
        {
            string mensaje = "";
            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_create_servicios", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Descripcion", reg.Descripcion);
                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = $"El servicio '{reg.Descripcion}' ha sido registrada correctamente.";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);
            }
            return mensaje;
        }

        public async Task<Servicio> Buscar(int id)
        {
            var lista = await Listar();
            var servicio = lista.FirstOrDefault(x => x.IdServicio == id);

            if (servicio == null)
                throw new Exception("Servicio no encontrado");

            return servicio;
        }

        public async Task<string> Eliminar(int id)
        {
            string mensaje = "";
            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_delete_servicios", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idServicio", id);
                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = "El servicio ha sido eliminada correctamente.";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);
            }
            return mensaje;
        }

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
