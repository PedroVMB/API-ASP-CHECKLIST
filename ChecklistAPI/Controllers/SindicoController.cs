using ChecklistAPI.Models;
using ChecklistAPI.Models.Dtos;
using ChecklistAPI.Repositories;
using ChecklistAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ChecklistAPI.Controllers
{
    [Authorize(Roles = "Sindico, Administrador")]
    [Route("[controller]")]
    [ApiController]
    public class SindicoController : ControllerBase
    {
        private readonly SindicoRepository _sindicoRepository;
        public SindicoController(SindicoRepository sindicoRepository)
        {
            _sindicoRepository = sindicoRepository ?? throw new ArgumentNullException(nameof(sindicoRepository));
        }


        [HttpPost]
        public async Task<IActionResult> CreateSindico([FromBody] SindicoDTO sindicoDTO)
        {
            try
            {
                await _sindicoRepository.CreateSindico(sindicoDTO);
                return Ok("Sindico criado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar sindico: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSindico(decimal id)
        {
            try
            {
                bool result = await _sindicoRepository.DeleteSindico(id);
                if (result)
                    return Ok("Sindico deletado com sucesso.");
                else
                    return NotFound("Sindico não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar Sindico: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSindicos()
        {
            try
            {
                var sindicos = await _sindicoRepository.GetSindicos();
                return Ok(sindicos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter sindico: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSindicoById(decimal id)
        {
            try
            {
                var Sindico = await _sindicoRepository.GetSindicoById(id);
                if (Sindico != null)
                    return Ok(Sindico);
                else
                    return NotFound("Sindico não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter sindico: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSindico(decimal id, [FromBody] Sindico sindico)
        {
            try
            {
                var updatedSindico = await _sindicoRepository.UpdateSindico(sindico, id);
                if (updatedSindico != null)
                    return Ok(updatedSindico);
                else
                    return NotFound("Sindico não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar sindico: {ex.Message}");
            }
        }
    }
}
