using ChecklistAPI.Models;

namespace ChecklistAPI.Repositories.Interfaces
{
    public interface ICondominioRepository
    {
        Task<Condominio> CreateCondominio(Condominio condominio);
        Task<List<Condominio>> GetCondominios();
        Task<Condominio> GetCondominiotById(int id);
        Task<Condominio> UpdateCondominio(Condominio condominio, int id);
        Task<bool> DeleteCondominio(int id);
    }
}
