using ChecklistAPI.Models;
using ChecklistAPI.Models.Dtos;

namespace ChecklistAPI.Repositories.Interfaces
{
    public interface ICondominioRepository
    {
        Task CreateCondominio(CondominoDTO condominoDTO);
        Task<List<Condominio>> GetCondominios();
        Task<Condominio> GetCondominiotById(int id);
        Task<Condominio> UpdateCondominio(Condominio condominio, int id);
        Task<bool> DeleteCondominio(int id);
    }
}
