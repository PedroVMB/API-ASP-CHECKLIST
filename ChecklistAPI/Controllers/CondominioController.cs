using ChecklistAPI.Models;
using ChecklistAPI.Models.Dtos;
using ChecklistAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ChecklistAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CondominioController : ControllerBase
    {
        private readonly CondominioRepository _condominioRepository;
        public CondominioController(CondominioRepository condominioRepository)
        {
            _condominioRepository = condominioRepository ?? throw new ArgumentNullException(nameof(condominioRepository));
        }

        [HttpPost]
        public async Task<IActionResult> CreateCondominio([FromBody] CondominoDTO condominoDTO)
        {
            try
            {
                await _condominioRepository.CreateCondominio(condominoDTO);
                return Ok("Condomínio criado com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar condomínio: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCondominio(int id)
        {
            try
            {
                bool result = await _condominioRepository.DeleteCondominio(id);
                if (result)
                    return Ok("Condomínio deletado com sucesso.");
                else
                    return NotFound("Condomínio não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar condomínio: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCondominios()
        {
            try
            {
                var condominios = await _condominioRepository.GetCondominios();
                return Ok(condominios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter condomínios: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCondominioById(int id)
        {
            try
            {
                var condominio = await _condominioRepository.GetCondominiotById(id);
                if (condominio != null)
                    return Ok(condominio);
                else
                    return NotFound("Condomínio não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter condomínio: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCondominio(int id, [FromBody] Condominio condominio)
        {
            try
            {
                var updatedCondominio = await _condominioRepository.UpdateCondominio(condominio, id);
                if (updatedCondominio != null)
                    return Ok(updatedCondominio);
                else
                    return NotFound("Condomínio não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar condomínio: {ex.Message}");
            }
        }
    }
}
