using ChecklistAPI.Models.Dtos;
using ChecklistAPI.Models;

namespace ChecklistAPI.Repositories.Interfaces
{
    public interface IRegistroRepository
    {
        Task CreateRegistro(RegistroDTO RegistroDTO);
        Task<List<Registro>> GetRegistros();
        Task<Registro> GetRegistroById(decimal id);
        Task<Registro> UpdateRegistro(Registro Registro, decimal id);
        Task<bool> DeleteRegistro(decimal id);
    }
}
