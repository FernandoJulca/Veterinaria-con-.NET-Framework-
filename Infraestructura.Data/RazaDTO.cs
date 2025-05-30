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
    public class RazaDTO : IRaza
    {
        public async Task<string> Actualizar(Raza reg)
        {
            string mensaje = "";
            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_update_raza", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRaza", reg.IdRaza);
                        cmd.Parameters.AddWithValue("@Descripcion", reg.NombreRaza);
                        cmd.Parameters.AddWithValue("@IdEspecie", reg.IdEspecie);
                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = $"La raza '{reg.NombreRaza}' ha sido actualizada correctamente.";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);
            }
            return mensaje;
        }

        public async Task<string> Agregar(Raza reg)
        {
            string mensaje = "";
            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_create_raza", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Descripcion", reg.NombreRaza);
                        cmd.Parameters.AddWithValue("@IdEspecie", reg.IdEspecie);
                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = $"La raza '{reg.NombreRaza}' ha sido registrada correctamente.";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);
            }
            return mensaje;
        }

        public async Task<Raza> Buscar(int id)
        {
            var lista = await Listar();
            var Raza = lista.FirstOrDefault(x => x.IdRaza == id);

            if (Raza == null)
                throw new Exception("Raza no encontrado");

            return Raza;
        }

        public async Task<string> Eliminar(int id)
        {
            string mensaje = "";
            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_delete_raza", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdRaza", id);
                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = "La raza ha sido eliminada correctamente.";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);
            }
            return mensaje;
        }

        public async Task<IEnumerable<Raza>> Listar()
        {
            List<Raza> Razas = new List<Raza>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_listar_raza", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            while (dr.Read())
                            {
                                Razas.Add(new Raza()
                                {
                                    IdRaza = dr.GetInt32(0),
                                    NombreRaza = dr.GetString(1),
                                    IdEspecie = dr.GetInt32(2),
                                    NombreEspecie = dr.GetString(3)
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
                throw new Exception("Error al listar Raza", ex);
            }
            return Razas;

        }
    }
}
