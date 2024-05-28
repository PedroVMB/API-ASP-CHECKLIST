using ChecklistAPI.Models;
using ChecklistAPI.Models.Dtos;
using ChecklistAPI.Models.Enums;
using ChecklistAPI.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ChecklistAPI.Repositories
{
    public class RegistroRepository : IRegistroRepository
    {
        private readonly string _connectionString;

        public RegistroRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:SqlServerDb"] ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task CreateRegistro(RegistroDTO registroDTO)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Verificar se o condomínio existe
                            string checkCondominioSql = "SELECT COUNT(*) FROM condominio WHERE id = @idCondominio";
                            using (var checkCondominioCommand = new SqlCommand(checkCondominioSql, connection, transaction))
                            {
                                checkCondominioCommand.Parameters.AddWithValue("@idCondominio", registroDTO.CondominioId);

                                int condominioCount = (int)await checkCondominioCommand.ExecuteScalarAsync();
                                if (condominioCount == 0)
                                {
                                    throw new Exception("Condomínio não encontrado");
                                }
                            }

                            // Verificar se a torre existe
                            string checkTorreSql = "SELECT COUNT(*) FROM torre WHERE id = @idTorre";
                            using (var checkTorreCommand = new SqlCommand(checkTorreSql, connection, transaction))
                            {
                                checkTorreCommand.Parameters.AddWithValue("@idTorre", registroDTO.TorreId);

                                int torreCount = (int)await checkTorreCommand.ExecuteScalarAsync();
                                if (torreCount == 0)
                                {
                                    throw new Exception("Torre não encontrada");
                                }
                            }

                            // Inserir o registro se o condomínio e a torre existem
                            string sql = @"INSERT INTO registro 
                                           (condominio_id, torre_id, foto_path, data_do_registro, descricao_problema, tipo_problema) 
                                           VALUES 
                                           (@idCondominio, @idTorre, @fotoPath, @dataDoRegistro, @descricaoProblema, @tipoProblema)";

                            using (var command = new SqlCommand(sql, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@idCondominio", registroDTO.CondominioId);
                                command.Parameters.AddWithValue("@idTorre", registroDTO.TorreId);
                                command.Parameters.AddWithValue("@fotoPath", registroDTO.FotoPath);
                                command.Parameters.AddWithValue("@dataDoRegistro", registroDTO.DataDoRegistro);
                                command.Parameters.AddWithValue("@descricaoProblema", registroDTO.DescricaoProblema);
                                command.Parameters.AddWithValue("@tipoProblema", registroDTO.TipoProblema);

                                await command.ExecuteNonQueryAsync();
                            }

                            transaction.Commit();
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteRegistro(decimal id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = "DELETE FROM registro WHERE id = @Id";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Registro> GetRegistroById(decimal id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = "SELECT * FROM registro WHERE id = @Id";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new Registro
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    CondominioId = Convert.ToInt32(reader["condominio_id"]),
                                    TorreId = Convert.ToInt32(reader["torre_id"]),
                                    FotoPath = reader["foto_path"].ToString(),
                                    DataDoRegistro = Convert.ToDateTime(reader["data_do_registro"]),
                                    DescricaoProblema = reader["descricao_problema"].ToString(),
                                    TipoProblema = (ProblemaEnum)Enum.Parse(typeof(ProblemaEnum), reader["tipo_problema"].ToString()),
                                    CreatedAt = Convert.ToDateTime(reader["created_at"]),
                                    UpdatedAt = Convert.ToDateTime(reader["updated_at"])
                                };
                            }
                            return null;
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Registro>> GetRegistros()
        {
            try
            {
                var registros = new List<Registro>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = "SELECT * FROM registro";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                registros.Add(new Registro
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    CondominioId = Convert.ToInt32(reader["condominio_id"]),
                                    TorreId = Convert.ToInt32(reader["torre_id"]),
                                    FotoPath = reader["foto_path"].ToString(),
                                    DataDoRegistro = Convert.ToDateTime(reader["data_do_registro"]),
                                    DescricaoProblema = reader["descricao_problema"].ToString(),
                                    TipoProblema = (ProblemaEnum)Enum.Parse(typeof(ProblemaEnum), reader["tipo_problema"].ToString()),
                                    CreatedAt = Convert.ToDateTime(reader["created_at"]),
                                    UpdatedAt = Convert.ToDateTime(reader["updated_at"])
                                });
                            }
                        }
                    }
                }

                return registros;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Registro> UpdateRegistro(Registro registro, decimal id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = @"UPDATE registro SET 
                                    condominio_id = @condominio_id, 
                                    torre_id = @torre_id, 
                                    foto_path = @foto_path, 
                                    data_do_registro = @data_do_registro, 
                                    descricao_problema = @descricao_problema, 
                                    tipo_problema = @tipo_problema
                                    WHERE id = @Id";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@condominio_id", registro.CondominioId);
                        command.Parameters.AddWithValue("@torre_id", registro.TorreId);
                        command.Parameters.AddWithValue("@foto_path", registro.FotoPath);
                        command.Parameters.AddWithValue("@data_do_registro", registro.DataDoRegistro);
                        command.Parameters.AddWithValue("@descricao_problema", registro.DescricaoProblema);
                        command.Parameters.AddWithValue("@tipo_problema", registro.TipoProblema);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                            return registro;

                        return null;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
