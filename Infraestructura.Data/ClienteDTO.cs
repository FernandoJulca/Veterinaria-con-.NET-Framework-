using System;
using System.Collections.Generic;
using System.Configuration;
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
        public async Task<Cliente> IniciarSesion(string correo,string contrasenia)
        {
            Cliente cli = new Cliente();
            string message = "";
            try
            {
                using(SqlConnection cnn = new SqlConnection(ConfigurationManager.ConnectionStrings["cadena"].ConnectionString))
                {
                    await cnn.OpenAsync();
                    using(SqlCommand cmd = new SqlCommand("usp_iniciar_session", cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Correo", correo);
                        cmd.Parameters.AddWithValue("@Contrasenia", contrasenia);

                        using(SqlDataReader reader = await cmd.ExecuteReaderAsync())
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
            catch(Exception ex)
            {
                message = ex.Message;
            }
            return cli;
        }

        public Task<IEnumerable<Cliente>> Listar()
        {
            throw new NotImplementedException();
        }
    }
}
