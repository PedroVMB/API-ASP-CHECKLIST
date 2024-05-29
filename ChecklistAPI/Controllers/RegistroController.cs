using ChecklistAPI.Models;
using ChecklistAPI.Models.Dtos;
using ChecklistAPI.Repositories;
using ChecklistAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChecklistAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RegistroController : ControllerBase
    {
        private readonly RegistroRepository _registroRepository;
        public RegistroController(RegistroRepository registroRepository)
        {
            _registroRepository = registroRepository ?? throw new ArgumentNullException(nameof(registroRepository));
        }

        [HttpPost]
        public async Task<IActionResult> CreateRegistro([FromBody] RegistroDTO registroDTO)
        {
            try
            {
                await _registroRepository.CreateRegistro(registroDTO);
                return Ok("Registro criado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar registro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistro(decimal id)
        {
            try
            {
                bool result = await _registroRepository.DeleteRegistro(id);
                if (result)
                    return Ok("Registro deletado com sucesso.");
                else
                    return NotFound("Registro não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar Registro: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetRegistros()
        {
            try
            {
                var registros = await _registroRepository.GetRegistros();
                return Ok(registros);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter registros: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegistroById(decimal id)
        {
            try
            {
                var registro = await _registroRepository.GetRegistroById(id);
                if (registro != null)
                    return Ok(registro);
                else
                    return NotFound("Registro não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter registro: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegistro(decimal id, [FromBody] Registro registro)
        {
            try
            {
                var updateRegistro = await _registroRepository.UpdateRegistro(registro, id);
                if (updateRegistro != null)
                    return Ok(updateRegistro);
                else
                    return NotFound("Registro não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar registro: {ex.Message}");
            }
        }
    }
}
