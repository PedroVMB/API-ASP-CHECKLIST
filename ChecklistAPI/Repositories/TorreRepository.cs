using ChecklistAPI.Models;
using ChecklistAPI.Models.Dtos;
using ChecklistAPI.Repositories.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ChecklistAPI.Repositories
{
    public class TorreRepository : ITorreRepository
    {
        private readonly string _connectionString;

        public TorreRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:SqlServerDb"] ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task CreateTorre(TorreDTO torreDTO)
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
                            string checkCondominioSql = "SELECT COUNT(*) FROM condominios WHERE id = @idCondominio";
                            using (var checkCommand = new SqlCommand(checkCondominioSql, connection, transaction))
                            {
                                checkCommand.Parameters.AddWithValue("@idCondominio", torreDTO.Condominio_id);

                                int condominioCount = (int)await checkCommand.ExecuteScalarAsync();
                                if (condominioCount == 0)
                                {
                                    throw new Exception("Condomínio não encontrado");
                                }
                            }

                            // Inserir a torre se o condomínio existe
                            string sql = @"INSERT INTO torres 
                                   (condominio_id, numero_torre, quantidade_andares, quantidade_terracos, 
                                    quantidade_salao_de_festas, quantidade_garagens, quantidade_guaritas) 
                                   VALUES 
                                   (@idCondominio, @numeroTorre, @quantidadeAndar, @quantidadeTerraco, 
                                    @quantidadeSalaoDeFesta, @quantidadeGaragem, @quantidadeGuarita)";

                            using (var command = new SqlCommand(sql, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@idCondominio", torreDTO.Condominio_id);
                                command.Parameters.AddWithValue("@numeroTorre", torreDTO.Numero_torre);
                                command.Parameters.AddWithValue("@quantidadeAndar", torreDTO.Quantidade_andares);
                                command.Parameters.AddWithValue("@quantidadeTerraco", torreDTO.Quantidade_terracos);
                                command.Parameters.AddWithValue("@quantidadeSalaoDeFesta", torreDTO.Quantidade_salao_de_festas);
                                command.Parameters.AddWithValue("@quantidadeGaragem", torreDTO.Quantidade_garagens);
                                command.Parameters.AddWithValue("@quantidadeGuarita", torreDTO.Quantidade_guaritas);

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




        public async Task<bool> DeleteTorre(decimal id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = "DELETE FROM torres WHERE id = @Id";

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

        public Task<bool> DeleteTorre(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Torre> GetTorreById(decimal id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = "SELECT * FROM torres WHERE id = @Id";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new Torre
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    Condominio_id = Convert.ToInt32(reader["condominio_id"]),
                                    Numero_torre = Convert.ToInt32(reader["numero_torre"]),
                                    Quantidade_andares = Convert.ToInt32(reader["quantidade_andares"]),
                                    Quantidade_terracos = Convert.ToInt32(reader["quantidade_terracos"]),
                                    Quantidade_salao_de_festas = Convert.ToInt32(reader["quantidade_salao_de_festas"]),
                                    Quantidade_garagens = Convert.ToInt32(reader["quantidade_garagens"]),
                                    Quantidade_guaritas = Convert.ToInt32(reader["quantidade_guaritas"])
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

        public Task<Torre> GetTorreById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Torre>> GetTorres()
        {
            try
            {
                var torres = new List<Torre>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = "SELECT * FROM torres";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                torres.Add(new Torre
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    Condominio_id = Convert.ToInt32(reader["condominio_id"]),
                                    Numero_torre = Convert.ToInt32(reader["numero_torre"]),
                                    Quantidade_andares = Convert.ToInt32(reader["quantidade_andares"]),
                                    Quantidade_terracos = Convert.ToInt32(reader["quantidade_terracos"]),
                                    Quantidade_salao_de_festas = Convert.ToInt32(reader["quantidade_salao_de_festas"]),
                                    Quantidade_garagens = Convert.ToInt32(reader["quantidade_garagens"]),
                                    Quantidade_guaritas = Convert.ToInt32(reader["quantidade_guaritas"])
                                });
                            }
                        }
                    }
                }

                return torres;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Torre> UpdateTorre(Torre torre, decimal id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = @"UPDATE torres SET 
                                    condominio_id = @condominio_id, 
                                    numero_torre = @numero_torre, 
                                    quantidade_andares = @quantidade_andares, 
                                    quantidade_terracos = @quantidade_terracos, 
                                    quantidade_salao_de_festas = @quantidade_salao_de_festas, 
                                    quantidade_garagens = @quantidade_garagens, 
                                    quantidade_guaritas = @quantidade_guaritas 
                                    WHERE id = @Id";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        command.Parameters.AddWithValue("@condominio_id", torre.Condominio_id);
                        command.Parameters.AddWithValue("@numero_torre", torre.Numero_torre);
                        command.Parameters.AddWithValue("@quantidade_andares", torre.Quantidade_andares);
                        command.Parameters.AddWithValue("@quantidade_terracos", torre.Quantidade_terracos);
                        command.Parameters.AddWithValue("@quantidade_salao_de_festas", torre.Quantidade_salao_de_festas);
                        command.Parameters.AddWithValue("@quantidade_garagens", torre.Quantidade_garagens);
                        command.Parameters.AddWithValue("@quantidade_guaritas", torre.Quantidade_guaritas);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                            return torre;

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
