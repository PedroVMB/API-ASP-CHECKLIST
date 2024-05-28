using ChecklistAPI.Models;
using ChecklistAPI.Models.Dtos;

namespace ChecklistAPI.Repositories.Interfaces
{
    public interface ICondominioRepository
    {
        Task CreateCondominio(CondominoDTO condominoDTO);
        Task<List<Condominio>> GetCondominios();
        Task<Condominio> GetCondominiotById(decimal id);
        Task<Condominio> UpdateCondominio(Condominio condominio, decimal id);
        Task<bool> DeleteCondominio(decimal id);
    }
}
