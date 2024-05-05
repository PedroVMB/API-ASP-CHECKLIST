using ChecklistAPI.Models;
using ChecklistAPI.Models.Dtos;

namespace ChecklistAPI.Repositories.Interfaces
{
    public interface ITorreRepository
    {
        Task CreateTorre(TorreDTO torreDTO);
        Task<List<Torre>> GetTorres();
        Task<Torre> GetTorreById(int id);
        Task<Torre> UpdateTorre(Torre torre, int id);
        Task<bool> DeleteTorre(int id);
    }
}
