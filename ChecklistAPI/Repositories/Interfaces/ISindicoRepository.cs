using ChecklistAPI.Models;
using ChecklistAPI.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChecklistAPI.Repositories.Interfaces
{
    public interface ISindicoRepository
    {
        Task CreateSindico(SindicoDTO sindicoDTO);
        Task<bool> DeleteSindico(decimal id);
        Task<Sindico> GetSindicoById(decimal id);
        Task<List<Sindico>> GetSindicos();
        Task<Sindico> UpdateSindico(Sindico sindico, decimal id);
    }
}
