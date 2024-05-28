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

                    string sql = @"INSERT INTO condominios 
                           (nome, cnpj, bairro, cep, complemento, numero, uf, cidade, quantidade_torres) 
                           VALUES 
                           (@nome, @cnpj, @bairro, @cep, @complemento, @numero, @uf, @cidade, @quantidade_torres)";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@nome", condominoDTO.Nome);
                        command.Parameters.AddWithValue("@cnpj", condominoDTO.Cnpj);
                        command.Parameters.AddWithValue("@bairro", condominoDTO.Bairro);
                        command.Parameters.AddWithValue("@cep", condominoDTO.Cep);
                        command.Parameters.AddWithValue("@complemento", condominoDTO.Complemento);
                        command.Parameters.AddWithValue("@numero", condominoDTO.Numero);
                        command.Parameters.AddWithValue("@uf", condominoDTO.Uf);
                        command.Parameters.AddWithValue("@cidade", condominoDTO.Cidade);
                        command.Parameters.AddWithValue("@quantidade_torres", condominoDTO.Quantidade_de_torres);

                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public async Task<bool> DeleteCondominio(decimal id)
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
                                    Bairro = reader["bairro"].ToString(),
                                    Cep = reader["cep"].ToString(),
                                    Complemento = reader["complemento"].ToString(),
                                    Numero = reader["numero"].ToString(),
                                    Uf = reader["uf"].ToString(),
                                    Cidade = reader["cidade"].ToString(),
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


        public async Task<Condominio> GetCondominiotById(decimal id)
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
                                    Bairro = reader["bairro"].ToString(),
                                    Cep = reader["cep"].ToString(),
                                    Complemento = reader["complemento"].ToString(),
                                    Numero = reader["numero"].ToString(),
                                    Uf = reader["uf"].ToString(),
                                    Cidade = reader["cidade"].ToString(),
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


        public async Task<Condominio> UpdateCondominio(Condominio condominio, decimal id)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    string sql = @"UPDATE condominios SET 
                           nome = @nome, 
                           cnpj = @cnpj, 
                           bairro = @bairro, 
                           cep = @cep, 
                           complemento = @complemento, 
                           numero = @numero, 
                           uf = @uf, 
                           cidade = @cidade, 
                           quantidade_torres = @quantidade_torres 
                           WHERE id = @id";

                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@nome", condominio.Nome);
                        command.Parameters.AddWithValue("@cnpj", condominio.Cnpj);
                        command.Parameters.AddWithValue("@bairro", condominio.Bairro);
                        command.Parameters.AddWithValue("@cep", condominio.Cep);
                        command.Parameters.AddWithValue("@complemento", condominio.Complemento);
                        command.Parameters.AddWithValue("@numero", condominio.Numero);
                        command.Parameters.AddWithValue("@uf", condominio.Uf);
                        command.Parameters.AddWithValue("@cidade", condominio.Cidade);
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
