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
    public class EspecieDTO : IEspecie
    {
        public async Task<string> Actualizar(Especie reg)
        {
            string mensaje = "";
            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_update_Especie", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdEspecie", reg.IdEspecie);
                        cmd.Parameters.AddWithValue("@Descripcion", reg.NombreEspecie);
                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = $"La especie '{reg.NombreEspecie}' ha sido actualizada correctamente.";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);
            }
            return mensaje;
        }

        public async Task<string> Agregar(Especie reg)
        {
            string mensaje = "";
            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_create_especie", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Descripcion", reg.NombreEspecie);
                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = $"La especie '{reg.NombreEspecie}' ha sido registrada correctamente.";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);
            }
            return mensaje;
        }

        public async Task<Especie> Buscar(int id)
        {
            var lista = await Listar();
            var Especie = lista.FirstOrDefault(x => x.IdEspecie == id);

            if (Especie == null)
                throw new Exception("Especie no encontrado");

            return Especie;
        }

        public async Task<string> Eliminar(int id)
        {
            string mensaje = "";
            try
            {

                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_delete_especie", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@IdEspecie", id);
                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = "La especie ha sido eliminada correctamente.";
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error en la base de datos", ex);
            }
            return mensaje;
        }

        public async Task<IEnumerable<Especie>> Listar()
        {
            List<Especie> Especies = new List<Especie>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_listar_especie", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            while (dr.Read())
                            {
                                Especies.Add(new Especie()
                                {
                                    IdEspecie = dr.GetInt32(0),
                                    NombreEspecie = dr.GetString(1)
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
                throw new Exception("Error al listar Especie", ex);
            }
            return Especies;

        }
    }
}
