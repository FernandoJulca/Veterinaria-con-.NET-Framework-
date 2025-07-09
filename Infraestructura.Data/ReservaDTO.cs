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
    public class ReservaDTO : IReserva
    {
        public async Task<IEnumerable<Reserva>> Listar()
        {
            throw new NotImplementedException();
        }

        public async Task<string> crearReserva(Reserva r)
        {
            string mensaje = "";
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_crear_reserva", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Fecha", r.fecha);
                        cmd.Parameters.AddWithValue("@Motivo", r.motivo);
                        cmd.Parameters.AddWithValue("@IdCliente", r.idCliente);
                        cmd.Parameters.AddWithValue("@IdEspecie", r.idAnimal);
                        cmd.Parameters.AddWithValue("@NombreMascota", r.nombreMascota);
                        cmd.Parameters.AddWithValue("@IdServicio", r.idServicio);
                        cmd.Parameters.AddWithValue("@IdVeterinario", r.idVeterinario);
                        int i = await cmd.ExecuteNonQueryAsync();
                        mensaje = $"La reserva ha sido confirmada";
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return mensaje;
        }

        public async Task<List<Reserva>> listaHistorialAdmin()
        {
            List<Reserva> historia = new List<Reserva>();
            try
            {
                using(SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using(SqlCommand cmd = new SqlCommand("usp_historial_reservas_admin", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                int IdAtencion = reader.GetInt32(reader.GetOrdinal("IdAtencion"));
                                var atencion = historia.FirstOrDefault(v => v.idAtencion == IdAtencion);
                                if (atencion == null)
                                {
                                    atencion = new Reserva
                                    {
                                        idAtencion = IdAtencion,
                                        fecha = reader.GetDateTime(reader.GetOrdinal("Fecha")),
                                        motivo = reader.GetString(reader.GetOrdinal("Motivo")),
                                        nombreCliente = reader.GetString(reader.GetOrdinal("NombreCliente")),
                                        apellidoCliente = reader.GetString(reader.GetOrdinal("ApellidoCliente")),
                                        nombreAnimal = reader.GetString(reader.GetOrdinal("Animal")),
                                        nombreMascota = reader.GetString(reader.GetOrdinal("NombreMascota")),
                                        nombreServicio = reader.GetString(reader.GetOrdinal("Descripcion")),
                                        nombreVeterinario = reader.GetString(reader.GetOrdinal("Veterinario"))
                                    };
                                    historia.Add(atencion);
                                }
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Error al listar veterinarios", ex);

            }
            return historia;
        }
    }
}
