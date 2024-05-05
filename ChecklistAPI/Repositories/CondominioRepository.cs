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
    public class CondominioRepository : ICondominioRepository
    {
        private readonly string _connectionString;
        public CondominioRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:SqlServerDb"] ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task CreateCondominio(CondominoDTO condominoDTO)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = @"INSERT INTO condominios " +
                                 "(nome, cnpj, quantidade_torres) VALUES" +
                                 "(@nome, @cnpj, @quantidade_torres)";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", condominoDTO.Nome);
                        command.Parameters.AddWithValue("@cnpj", condominoDTO.Cnpj);
                        command.Parameters.AddWithValue("@quantidade_torres", condominoDTO.Quantidade_de_torres);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Condominio> CreateCondominio(Condominio condominio)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteCondominio(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = @"DELETE FROM condominios WHERE id = @id";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

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

        public async Task<List<Condominio>> GetCondominios()
        {
            try
            {
                var condominios = new List<Condominio>();

                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = "SELECT * FROM condominios";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (await reader.ReadAsync())
                            {
                                var condominio = new Condominio
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    Nome = reader["nome"].ToString(),
                                    Cnpj = reader["cnpj"].ToString(),
                                    Quantidade_de_torres = Convert.ToInt32(reader["quantidade_torres"])
                                };

                                condominios.Add(condominio);
                            }
                        }
                    }
                }

                return condominios;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Condominio> GetCondominiotById(int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = "SELECT * FROM condominios WHERE id = @id";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            if (await reader.ReadAsync())
                            {
                                return new Condominio
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    Nome = reader["nome"].ToString(),
                                    Cnpj = reader["cnpj"].ToString(),
                                    Quantidade_de_torres = Convert.ToInt32(reader["quantidade_torres"])
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
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Condominio> UpdateCondominio(Condominio condominio, int id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = @"UPDATE condominios SET nome = @nome, cnpj = @cnpj, quantidade_torres = @quantidade_torres WHERE id = @id";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@nome", condominio.Nome);
                        command.Parameters.AddWithValue("@cnpj", condominio.cnpj);
                        command.Parameters.AddWithValue("@quantidade_torres", condominio.Quantidade_de_torres);

                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            return condominio;
                        }
                        else
                        {
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

       
    }
}
