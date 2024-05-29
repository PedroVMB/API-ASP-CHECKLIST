using ChecklistAPI.Models;
using ChecklistAPI.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChecklistAPI.Repositories.Interfaces
{
    public interface IAdministradorRepository
    {
        Task CreateAdministrador(AdministradorDTO administradorDTO);
        Task<bool> DeleteAdministrador(decimal id);
        Task<Administrador> GetAdministradorById(decimal id);
        Task<List<Administrador>> GetAdministradores();
        Task<Administrador> UpdateAdministrador(Administrador administrador, decimal id);
    }
}
