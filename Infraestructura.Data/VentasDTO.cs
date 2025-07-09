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
    public class VentasDTO : IVenta
    {
        public async Task<IEnumerable<Venta>> ListarPorMes()
        {
            List<Venta> Ventas = new List<Venta>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_ventas_mes", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        using (SqlDataReader dr = await cmd.ExecuteReaderAsync())
                        {
                            while (dr.Read())
                            {
                                Ventas.Add(new Venta()
                                {
                                    Mes = dr.GetString(0),
                                    TotalMensual = dr.GetDecimal(1)
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
                throw new Exception("Error al listar Venta", ex);
            }
            return Ventas;
        }

        public async Task<List<Venta>> HistorialComprasAdmin()
        {
            List<Venta> historia = new List<Venta>();
            try
            {
                using (SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using (SqlCommand cmd = new SqlCommand("usp_historial_compras_admin", cnn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
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
                                        NombreCliente = reader.GetString(reader.GetOrdinal("NombreCliente")),
                                        ApellidoCliente = reader.GetString(reader.GetOrdinal("ApellidoCliente")),
                                        Detalles = new List<DetalleVenta>()
                                    };
                                    historia.Add(venta);
                                }

                                var detalle = new DetalleVenta
                                {
                                    idDetalleVenta = reader.GetInt32(reader.GetOrdinal("IdDetalle")),
                                    nombreProducto = reader.GetString(reader.GetOrdinal("NombreProducto")),
                                    precio = reader.GetDecimal(reader.GetOrdinal("Precio")),
                                    cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad"))
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

        public async Task<string> AgregarVenta(int idCliente, List<Carro> carrito)
        {
            string mensaje = "";
            using (SqlConnection cn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
            {
                await cn.OpenAsync();
                SqlTransaction tr = cn.BeginTransaction();

                try
                {
                    // 1. Insertar Venta
                    SqlCommand cmd = new SqlCommand("usp_agregar_venta", cn, tr);
                    cmd.CommandType = CommandType.StoredProcedure;

                    decimal total = carrito.Sum(c => c.SubTotal);
                    cmd.Parameters.AddWithValue("@Total", total);
                    cmd.Parameters.AddWithValue("@IdCliente", idCliente);

                    SqlParameter paramId = new SqlParameter("@IdVenta", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(paramId);

                    await cmd.ExecuteNonQueryAsync();
                    int idVenta = (int)paramId.Value;

                    // 2. Insertar Detalles
                    foreach (var item in carrito)
                    {
                        SqlCommand cmdDetalle = new SqlCommand("usp_agregar_detalle_venta", cn, tr);
                        cmdDetalle.CommandType = CommandType.StoredProcedure;

                        cmdDetalle.Parameters.AddWithValue("@IdVenta", idVenta);
                        cmdDetalle.Parameters.AddWithValue("@IdProducto", item.IdProducto);
                        cmdDetalle.Parameters.AddWithValue("@Cantidad", item.Cantidad);
                        cmdDetalle.Parameters.AddWithValue("@Precio", item.Precio);

                        await cmdDetalle.ExecuteNonQueryAsync();
                    }

                    tr.Commit();
                    mensaje = $"Venta registrada con éxito";
                }
                catch (Exception ex)
                {
                    tr.Rollback();
                    mensaje = "Error al registrar venta: " + ex.Message;
                }
            }

            return mensaje;
        }
    }


   
}
