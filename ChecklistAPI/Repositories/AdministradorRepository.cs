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
    public class AdministradorRepository : IAdministradorRepository
    {
        private readonly string _connectionString;
        public AdministradorRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:SqlServerDb"] ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task CreateAdministrador(AdministradorDTO administradorDTO)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = @"INSERT INTO administrador (nome, email, cpf) VALUES (@nome, @cpf, @email)";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", administradorDTO.Nome);
                        command.Parameters.AddWithValue("@email", administradorDTO.Email);
                        command.Parameters.AddWithValue("@cpf", administradorDTO.Cpf);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar administrador: {ex.Message}");
            }
        }

        public async Task<bool> DeleteAdministrador(decimal id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = @"DELETE FROM administrador WHERE id = @id";

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
                throw new Exception($"Erro ao deletar administrador: {ex.Message}");
            }
        }

        public async Task<Administrador> GetAdministradorById(decimal id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = "SELECT * FROM administrador WHERE id = @id";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new Administrador
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
                throw new Exception($"Erro ao obter administrador por ID: {ex.Message}");
            }
        }

        public async Task<List<Administrador>> GetAdministradores()
        {
            try
            {
                var administradores = new List<Administrador>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = "SELECT * FROM administrador";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                administradores.Add(new Administrador
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

                return administradores;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter administradores: {ex.Message}");
            }
        }

        public async Task<Administrador> UpdateAdministrador(Administrador administrador, decimal id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = @"UPDATE administrador SET nome = @nome, email = @email, cpf = @cpf WHERE id = @id";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@nome", administrador.Nome);
                        command.Parameters.AddWithValue("@email", administrador.Email);
                        command.Parameters.AddWithValue("@cpf", administrador.Cpf);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return administrador;
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
                throw new Exception($"Erro ao atualizar administrador: {ex.Message}");
            }
        }
    }
}
