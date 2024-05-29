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
    public class SindicoRepository : ISindicoRepository
    {
        private readonly string _connectionString;
        public SindicoRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:SqlServerDb"] ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task CreateSindico(SindicoDTO sindicoDTO)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = @"INSERT INTO sindico (nome, email, cpf) VALUES (@nome, @cpf, @email)";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", sindicoDTO.Nome);
                        command.Parameters.AddWithValue("@email", sindicoDTO.Email);
                        command.Parameters.AddWithValue("@cpf", sindicoDTO.Cpf);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar síndico: {ex.Message}");
            }
        }

        public async Task<bool> DeleteSindico(decimal id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = @"DELETE FROM sindico WHERE id = @id";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar síndico: {ex.Message}");
            }
        }

        public async Task<Sindico> GetSindicoById(decimal id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = "SELECT * FROM sindico WHERE id = @id";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new Sindico
                                {
                                    Id = Convert.ToDecimal(reader["id"]),
                                    Nome = reader["nome"].ToString(),
                                    Email = reader["email"].ToString(),
                                    Cpf = reader["cpf"].ToString()
                                };
                            }
                            else
                            {
                                return null;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter síndico por ID: {ex.Message}");
            }
        }

        public async Task<List<Sindico>> GetSindicos()
        {
            try
            {
                var sindicos = new List<Sindico>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = "SELECT * FROM sindico";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                sindicos.Add(new Sindico
                                {
                                    Id = Convert.ToDecimal(reader["id"]),
                                    Nome = reader["nome"].ToString(),
                                    Email = reader["email"].ToString(),
                                    Cpf = reader["cpf"].ToString()
                                });
                            }
                        }
                    }
                }

                return sindicos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter síndicos: {ex.Message}");
            }
        }

        public async Task<Sindico> UpdateSindico(Sindico sindico, decimal id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = @"UPDATE sindico SET nome = @nome, email = @email, cpf = @cpf WHERE id = @id";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@nome", sindico.Nome);
                        command.Parameters.AddWithValue("@email", sindico.Email);
                        command.Parameters.AddWithValue("@cpf", sindico.Cpf);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return sindico;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar síndico: {ex.Message}");
            }
        }

    }
}
