using ChecklistAPI.Models;
using ChecklistAPI.Models.Dtos;

namespace ChecklistAPI.Repositories.Interfaces
{
    public interface ITorreRepository
    {
        Task CreateTorre(TorreDTO torreDTO);
        Task<List<Torre>> GetTorres();
        Task<Torre> GetTorreById(decimal id);
        Task<Torre> UpdateTorre(Torre torre, decimal id);
        Task<bool> DeleteTorre(decimal id);
    }
}
