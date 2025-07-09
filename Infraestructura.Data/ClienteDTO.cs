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
    public class ClienteDTO : ICliente
    {
        public async Task<Cliente> IniciarSesion(string correo, string contrasenia)
        {
            Cliente cli = null;
            string message = "";
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_iniciar_session", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Correo", correo);
                        cmd.Parameters.AddWithValue("@Contrasenia", contrasenia);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                cli = new Cliente()
                                {
                                    IdCliente = reader.GetInt32(0),
                                    NombreCliente = reader.GetString(1),
                                    ApellidoCliente = reader.GetString(2),
                                    Documento = reader.GetString(3),
                                    Telefono = reader.GetString(4),
                                    Correo = reader.GetString(5),
                                    Contrasenia = reader.GetString(6),
                                    Direccion = reader.GetString(7),
                                    Tipo = reader.GetString(8)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return cli;
        }

        public async Task<Cliente> ObtenerCliente(int idCliente)
        {
            Cliente cli = null;
            string message = "";
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_obtener_cliente", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idCliente", idCliente);

                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                cli = new Cliente()
                                {
                                    IdCliente = reader.GetInt32(0),
                                    NombreCliente = reader.GetString(1),
                                    ApellidoCliente = reader.GetString(2),
                                    Documento = reader.GetString(3),
                                    Telefono = reader.GetString(4),
                                    Correo = reader.GetString(5),
                                    Contrasenia = reader.GetString(6),
                                    Direccion = reader.GetString(7),
                                    Tipo = reader.GetString(8)
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = ex.Message;
            }
            return cli;
        }

        public async Task<string> RegistrarCliente(Cliente c)
        {
            string mensaje = "";
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_registrar_usuario", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Nombre", c.NombreCliente);
                        cmd.Parameters.AddWithValue("@Apellido", c.ApellidoCliente);
                        cmd.Parameters.AddWithValue("@Documento", c.Documento);
                        cmd.Parameters.AddWithValue("@Telefono", c.Telefono);
                        cmd.Parameters.AddWithValue("@Correo", c.Correo);
                        cmd.Parameters.AddWithValue("@Contrasenia", c.Contrasenia);
                        cmd.Parameters.AddWithValue("@Direccion", c.Direccion);
                        var mensajeParam = cmd.Parameters.Add("@Mensaje", SqlDbType.VarChar, 255);
                        mensajeParam.Direction = ParameterDirection.Output;

                        await cmd.ExecuteNonQueryAsync();
                        mensaje = mensajeParam.Value.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return mensaje;
        }
        public async Task<IEnumerable<Cliente>> Listar()
        {
            List<Cliente> list = new List<Cliente>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_listar_cliente", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                list.Add(new Cliente()
                                {
                                    IdCliente = reader.GetInt32(0),
                                    NombreCliente = reader.GetString(1),
                                    ApellidoCliente = reader.GetString(2),
                                    Documento = reader.GetString(3),
                                    Telefono = reader.GetString(4),
                                    Correo = reader.GetString(5),
                                    Contrasenia = reader.GetString(6),
                                    Direccion = reader.GetString(7)
                                });
                            }
                            reader.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar clientes", ex);
            }
            return list;
        }

        public async Task<List<Venta>> HistorialCompras(int idCliente)
        {
            List<Venta> historia = new List<Venta>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_historial_compras", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idCliente", idCliente);
                        using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                int idVenta = reader.GetInt32(reader.GetOrdinal("IdVenta"));
                                var venta = historia.FirstOrDefault(v => v.IdVenta == idVenta);
                                if (venta == null)
                                {
                                    venta = new Venta
                                    {
                                        IdVenta = idVenta,
                                        Fecha = reader.GetDateTime(reader.GetOrdinal("Fecha")),
                                        Total = reader.GetDecimal(reader.GetOrdinal("Total")),
                                        Detalles = new List<DetalleVenta>()
                                    };
                                    historia.Add(venta);
                                }

                                var detalle = new DetalleVenta
                                {
                                    idDetalleVenta = reader.GetInt32(reader.GetOrdinal("IdDetalle")),
                                    nombreProducto = reader.GetString(reader.GetOrdinal("Nombre")),
                                    precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                                    cantidad = reader.GetInt32(reader.GetOrdinal("cantidad"))
                                };
                                venta.Detalles.Add(detalle);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar el historial", ex);
            }
            return historia;
        }

        public async Task<List<Reserva>> Historial_Citas(int idCliente)
        {
            List<Reserva> historia = new List<Reserva>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_historial_citas", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idCliente", idCliente);
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
            catch (Exception ex)
            {
                throw new Exception("Error al listar el historial", ex);
            }
            return historia;
        }
    }
}
