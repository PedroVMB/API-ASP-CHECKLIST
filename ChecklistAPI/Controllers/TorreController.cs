using ChecklistAPI.Models;
using ChecklistAPI.Models.Dtos;
using ChecklistAPI.Repositories;
using ChecklistAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChecklistAPI.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Route("[controller]")]
    [ApiController]
    public class TorreController : ControllerBase
    {
        private readonly TorreRepository _torreRepository;
        public TorreController(TorreRepository torreRepository)
        {
            _torreRepository = torreRepository ?? throw new ArgumentNullException(nameof(torreRepository));
        }

        [HttpPost]
        public async Task<IActionResult> CreateTorre([FromBody] TorreDTO torreDTO)
        {
            try
            {
                await _torreRepository.CreateTorre(torreDTO);
                return Ok("Torre criada com sucesso. ");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar torre: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTorre(decimal id)
        {
            try
            {
                bool result = await _torreRepository.DeleteTorre(id);
                if(result) 
                    return Ok("Torre deletada com sucesso. ");
                else 
                    return NotFound("Torre nao encontrada. ");
                
            }
            catch (Exception ex)
            {

                return StatusCode(500, $"Erro ao deletar torre: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetTorres()
        {
            try
            {
                var torres = await _torreRepository.GetTorres();
                return Ok(torres);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter torres: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTorreById(decimal id)
        {
            try
            {
                var torres = await _torreRepository.GetTorreById(id);
                if (torres != null)
                    return Ok(torres);
                else
                    return NotFound("torres não encontrada.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter torre: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTorre(decimal id, [FromBody] Torre torre)
        {
            try
            {
                var updateTorre = await _torreRepository.UpdateTorre(torre, id);
                if (updateTorre != null)
                    return Ok(updateTorre);
                else
                    return NotFound("torre não encontrado.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar torre: {ex.Message}");
            }
        }


    }
}
