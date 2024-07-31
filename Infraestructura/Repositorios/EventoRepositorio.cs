using CPCore.Entidades;
using CPCore.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace CPInfraestructura.Repositorios
{
    public class EventoRepository : IEventoRepository
    {
        private readonly string _connectionString;

        public EventoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Evento>> GetEventos()
        {
            List<Evento> eventos = new List<Evento>();
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("SP_TBL_EVENTOS_SELECCIONTODO", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                eventos.Add(new Evento
                                {
                                    IdEvento = reader.GetInt32(reader.GetOrdinal("ID_EVENTO")),
                                    FechaEvento = reader.GetDateTime(reader.GetOrdinal("FECHA_EVENTO")),
                                    LugarEvento = reader.GetString(reader.GetOrdinal("LUGAR_EVENTO")),
                                    DescripcionEvento = reader.GetString(reader.GetOrdinal("DESCRIPCION_EVENTO")),
                                    Precio = reader.GetDecimal(reader.GetOrdinal("PRECIO")),
                                    FechaCreacion = reader.GetDateTime(reader.GetOrdinal("FECHA_CREACION"))
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
           
            return eventos;
        }

   
        public async Task AgregarEvento(Evento evento)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("SP_TBL_EVENTOS_INSERTAR", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@FECHA_EVENTO", evento.FechaEvento);
                        command.Parameters.AddWithValue("@LUGAR_EVENTO", evento.LugarEvento);
                        command.Parameters.AddWithValue("@DESCRIPCION_EVENTO", evento.DescripcionEvento);
                        command.Parameters.AddWithValue("@PRECIO", evento.Precio);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {

            }
  
        }

        public async Task ActualizaEvento(Evento evento)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("SP_TBL_EVENTOS_ACTUALIZA", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ID_EVENTO", evento.IdEvento);
                        command.Parameters.AddWithValue("@FECHA_EVENTO", evento.FechaEvento);
                        command.Parameters.AddWithValue("@LUGAR_EVENTO", evento.LugarEvento);
                        command.Parameters.AddWithValue("@DESCRIPCION_EVENTO", evento.DescripcionEvento);
                        command.Parameters.AddWithValue("@PRECIO", evento.Precio);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        public async Task EliminarEvento(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var command = new SqlCommand("SP_TBL_EVENTOS_ELIMINA", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@ID_EVENTO", id);
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
