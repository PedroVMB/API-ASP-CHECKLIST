using ChecklistAPI.Models;
using ChecklistAPI.Models.Dtos;
using ChecklistAPI.Repositories;
using ChecklistAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ChecklistAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdministradorController : ControllerBase
    {
        private readonly AdministradorRepository _administradorRepository;
        public AdministradorController(AdministradorRepository administradorRepository)
        {
            _administradorRepository = administradorRepository ?? throw new ArgumentNullException(nameof(administradorRepository));
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdministrador([FromBody] AdministradorDTO AdministradorDTO)
        {
            try
            {
                await _administradorRepository.CreateAdministrador(AdministradorDTO);
                return Ok("Administrador criado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar Administrador: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdministrador(decimal id)
        {
            try
            {
                bool result = await _administradorRepository.DeleteAdministrador(id);
                if (result)
                    return Ok("Administrador deletado com sucesso.");
                else
                    return NotFound("Administrador não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar Administrador: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAdministradors()
        {
            try
            {
                var Administradors = await _administradorRepository.GetAdministradores();
                return Ok(Administradors);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter Administrador: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdministradorById(decimal id)
        {
            try
            {
                var Administrador = await _administradorRepository.GetAdministradorById(id);
                if (Administrador != null)
                    return Ok(Administrador);
                else
                    return NotFound("Administrador não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter Administrador: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdministrador(decimal id, [FromBody] Administrador Administrador)
        {
            try
            {
                var updatedAdministrador = await _administradorRepository.UpdateAdministrador(Administrador, id);
                if (updatedAdministrador != null)
                    return Ok(updatedAdministrador);
                else
                    return NotFound("Administrador não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar Administrador: {ex.Message}");
            }
        }
    }
}
